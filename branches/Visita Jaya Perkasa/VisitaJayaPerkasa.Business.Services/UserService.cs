using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Caching;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Security.Cryptography;

namespace VisitaJayaPerkasa.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheProvider _cacheProvider;

        private const string USER_KEY_FORMAT = "User:{0}"; //User:Username

        public UserService(IUserRepository userRepository, ICacheProvider cacheProvider)
        {
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

            User user = _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return false;
            }

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

            if (!_cacheProvider.TryGet(key, out user))
            {
                user = _userRepository.GetUserByUsername(username);

                if (user != null)
                {
                    _cacheProvider.Insert(key, user, DateTime.Now.AddMinutes(20));
                }
            }
            return user;
        }

        public List<Role> GetRolesByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User Id cannot be an empty guid.", "userId");
            }

            var roles = _userRepository.GetRolesByUserId(userId);

            if (roles == null || roles.Count() == 0)
            {
                return null;
            }

            return roles;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            if (users == null || users.Count() == 0)
            {
                return null;
            }

            return users;
        }

        public void SaveUser(User user, UserRole userRole)
        {
            _userRepository.SaveUser(user, userRole);
        }

        public void DeleteUser(Guid Id)
        {
            _userRepository.DeleteUser(Id);
        }
    }
}
