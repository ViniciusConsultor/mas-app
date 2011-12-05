using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage="Name must be fill")]
        public string Name { get; set; }
        public string Description { get; set;}
    }
}