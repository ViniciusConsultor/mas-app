﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Logging;
using Shipping.Business.Entities;
using System.Data.SqlClient;
using Shipping.Business.Entities.Collections;

namespace Shipping.Data.Sql
{
    public class SqlUserRepository : Shipping.Data.IUserRepository
    {
        private string _mainConnectionString;
        private Dictionary<string, string> _satelliteConnectionStrings;
        private ILogger _logger;

        public SqlUserRepository(string mainConnectionString, Dictionary<string, string> satelliteConnectionStrings, ILogger logger)
        {
            _mainConnectionString = mainConnectionString;
            _satelliteConnectionStrings = satelliteConnectionStrings;
            _logger = logger;
        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null, empty, or contain only whitespace.", "username");
            }
            _logger.DebugFormat(this, "Getting user for username '{0}' from database.", username);

            return SqlUtility.ExecuteXmlStoredProcedure<User>(_mainConnectionString, _logger, "Users_GetUserByUsernameXml", new List<SqlParameter>
                {
                    new SqlParameter("@username", username)
                }.ToArray()
                );
        }

        public UserCollection GetUsers()
        {
            _logger.DebugFormat(this, "Getting user list from database.");

            return SqlUtility.ExecuteXmlStoredProcedure<UserCollection>(_mainConnectionString, _logger, "Users_GetUsersXml");
        }

        public List<Role> GetRolesByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be an empty guid.", "userId");
            }

            _logger.DebugFormat(this, "Getting roles for user id '{0}' from database.", userId.ToString());

            return SqlUtility.ExecuteXmlStoredProcedure<RoleCollection>(_mainConnectionString, _logger, "Users_GetUserRolesByIdXml", new List<SqlParameter>
            {
                new SqlParameter("@Id", userId)
            }.ToArray()
            );

        }
    }
}