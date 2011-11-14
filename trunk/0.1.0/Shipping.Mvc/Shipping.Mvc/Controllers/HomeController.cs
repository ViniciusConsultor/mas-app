using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shipping.Logging;
using Shipping.Business.Services;

namespace Shipping.Mvc.Home.Controller
{
    public class HomeController : Shipping.Web.Mvc.Controller
    {
        private ILogger _logger;
        private IUserService _userService;

        public HomeController(ILogger logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public ActionResult Index(bool unauthorized = false)
        {
            return View();
        }

    }
}
