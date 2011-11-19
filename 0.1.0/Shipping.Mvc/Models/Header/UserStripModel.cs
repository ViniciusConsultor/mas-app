using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shipping.Mvc.Models.Header
{
    public class UserStripModel
    {
        public string FullName;

        public bool IsAdmin = false;
        public bool IsUser = false;

        public string Portal50Url;
    }
}