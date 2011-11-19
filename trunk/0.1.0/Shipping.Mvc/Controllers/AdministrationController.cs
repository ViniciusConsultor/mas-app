using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shipping.Mvc.Controllers
{
    public class AdministrationController : Shipping.Web.Mvc.Controller
    {
        //
        // GET: /Administration/

        public ActionResult Index()
        {
            return View();
        }

    }
}
