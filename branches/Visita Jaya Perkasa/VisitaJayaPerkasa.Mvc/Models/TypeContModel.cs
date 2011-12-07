using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class TypeContModel
    {
        [Required(ErrorMessage = "type code must be fill")]
        public string TypeCode { get; set; }

        [Required(ErrorMessage = "type name must be fill")]
        public string TypeName { get; set; }

        [ScaffoldColumn(false)]
        public int deleted { get; set; }
    }
}