using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Business.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the username and password are valid in the system, otherwise false.</returns>
        bool ValidateUser(string username, string password);

        User GetUserByUsername(string username);

        /// <summary>
        /// Gets the password hint.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Password hint</returns>
        string GetPasswordHint(string username);

        /// <summary>
        /// Gets the roles by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>List of roles by defined user</returns>
        List<Role> GetRolesByUserId(Guid userId);

        /// <summary>
        /// Get the user list
        /// </summary>
        /// <returns>List if Users</returns>
        UserCollection GetUsers();
    }
}
