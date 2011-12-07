using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc; 

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class LeadTimeModel
    {
        [Required(ErrorMessage="Pelase fill city code")]
        public string CityCode { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage="Must be fill in numeric")]
        public int Days { get; set; }

        [ScaffoldColumn(false)]
        public int Deleted { get; set; }

        public List<SelectListItem> ListItems { get; set; }
    }
}