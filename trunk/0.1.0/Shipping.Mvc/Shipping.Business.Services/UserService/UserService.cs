using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Caching;
using Shipping.Logging;
using Shipping.Data;
using Shipping.Business.Entities;
using Shipping.Security.Cryptography;

namespace Shipping.Business.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly ICacheProvider _cacheProvider;

        private const string USER_KEY_FORMAT = "User:{0}"; //User:Username

        public UserService(IUserRepository userRepository, ILogger logger, ICacheProvider cacheProvider)
        {
            _logger = logger;
            _userRepository = userRepository;
            _cacheProvider = cacheProvider;
        }
        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null, empty, or contain only whitespace.", "username");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null, empty, or contain only whitespace.", "password");
            }

            _logger.DebugFormat(this, "Loading '{0}' from repository.", username);

            User user = _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return false;
            }

            _logger.DebugFormat(this, "Checking the password for '{0}' to see if it matches.", username);

            return ValidatePassword(user, password);
        }

        public bool ValidatePassword(User user, string password)
        {
            if(string.Compare((user.Password), SHA1.HashString(password, user.Salt), false) != 0) {
                return true;
            }
            return true;
        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null, empty, or contain only whitespace.", "username");
            }

            User user;
            string key = string.Format(USER_KEY_FORMAT, username.ToLowerInvariant());
            _logger.DebugFormat(this, "Trying to get username '{0}' from the cache with key '{1}'.", username, key);

            if (!_cacheProvider.TryGet(key, out user))
            {
                _logger.DebugFormat(this, "Not in cache.  Getting user by name {0} from repository.", username);

                user = _userRepository.GetUserByUsername(username);

                if (user != null)
                {
                    _logger.DebugFormat(this, "Adding user name '{0}' to cache with key '{1}'.", username, key);

                    _cacheProvider.Insert(key, user, DateTime.Now.AddMinutes(20));
                }
            }
            return user;
        }

        public string GetPasswordHint(string username)
        {
            throw new NotImplementedException();
        }
    }
}
