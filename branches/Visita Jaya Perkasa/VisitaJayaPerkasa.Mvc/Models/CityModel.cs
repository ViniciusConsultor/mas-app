using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class CityModel
    {
        [Required(ErrorMessage="Please fill city code")]
        public string CityCode { get; set; }
        public string CityName { get; set; }
    }
}