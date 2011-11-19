using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Shipping.Mvc.Models.Home
{
    public class HomeIndexModel
    {
        public bool Unauthorized { get; set; }

        [DisplayName("Do not show me this screen again")]
        public bool DisableIntroduction { get; set; }
    }
}