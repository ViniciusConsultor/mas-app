using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class VesselModel
    {
        [Required(ErrorMessage = "Please fill vessel code")]
        public string VesselCode { set; get; }

        [Required(ErrorMessage = "Please fill vessel name")]
        public string VesselName { set; get; }

        [ScaffoldColumn(false)]
        public int Deleted { get; set; }
    }
}