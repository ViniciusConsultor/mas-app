using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Services;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class HomeController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
