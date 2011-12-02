using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Mvc.Models.Role
{
    public class RoleModel
    {
        [Required(ErrorMessage="Name must be fill")]
        public string Name { get; set; }
        public string Description { get; set;}
    }
}