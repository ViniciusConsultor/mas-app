using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class TypeContModel
    {
        [Required(ErrorMessage="type code must be fill")]
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
    }
}