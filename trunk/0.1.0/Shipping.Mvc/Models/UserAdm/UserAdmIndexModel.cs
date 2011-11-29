using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shipping.Business.Entities.Collections;

namespace Shipping.Mvc.Models.UserAdm
{
    public class UserAdmIndexModel
    {
        public UserCollection Users { get; set; }
    }
}