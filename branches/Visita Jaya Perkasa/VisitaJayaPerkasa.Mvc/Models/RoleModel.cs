using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class RoleModel
    {
        [Required(ErrorMessage = "Name must be fill")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description must be fill")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public int deleted { get; set; }
    }
}