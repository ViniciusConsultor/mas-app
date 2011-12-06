﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface IUserRepository
    {
        void SaveUser(User user);

        User GetUserByUsername(string username);

        IEnumerable<User> GetAllUsers();

        List<Role> GetRolesByUserId(Guid userId);
    }
}