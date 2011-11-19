using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;

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
        /// <returns></returns>
        string GetPasswordHint(string username);

        /// <summary>
        /// Gets the roles by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Role> GetRolesByUserId(Guid userId);


    }
}
