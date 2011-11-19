using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;

namespace Shipping.Data
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);

        List<Role> GetRolesByUserId(Guid userId);
    }
}
