using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Data
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);

        UserCollection GetUsers();

        List<Role> GetRolesByUserId(Guid userId);
    }
}
