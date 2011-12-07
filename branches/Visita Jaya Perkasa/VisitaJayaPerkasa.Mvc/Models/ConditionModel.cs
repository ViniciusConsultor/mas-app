using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class ConditionModel
    {
        [Required(ErrorMessage="Please fill condition code")]
        public string ConditionCode { get; set; }
        public string ConditionName { get; set; }

        [ScaffoldColumn(false)]
        public int Deleted { get; set; }
    }
}