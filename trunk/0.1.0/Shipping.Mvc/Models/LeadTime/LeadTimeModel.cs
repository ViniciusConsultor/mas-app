using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Mvc.Models.LeadTime
{
    public class LeadTimeModel
    {
        [Required(ErrorMessage="Pelase fill city code")]
        public string CityCode { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage="Must be fill in numeric")]
        public int Days { get; set; }
    }
}