using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shipping.Business.Services;
using Shipping.Web.Security;
using Shipping.Configuration;
using Shipping.Logging;
using Shipping.Web.Mvc;
using Shipping.Mvc.Models.LogIn;

namespace Shipping.Mvc.Controllers
{
    public class LogInController : Shipping.Mvc.Controllers.Controller
    {
        private ILogger _logger;
        private IUserService _userService;
        private IFormsAuthenticationService _formsAuthenticationService;
        private IConfigurationManager _configurationManager;
        private bool _persistentCookie = false;

        public LogInController(ILogger logger, IUserService userService, IFormsAuthenticationService formsAuthenticationService, IConfigurationManager configurationManager)
            : base(logger, userService, formsAuthenticationService, configurationManager)
        {
            _logger = logger;
            _userService = userService;
            _formsAuthenticationService = formsAuthenticationService;
            _configurationManager = configurationManager;

            //Initialize the persistent cookie setting so that it can be reused.
            _persistentCookie = _configurationManager.GetBoolean("Shipping.Login.Mvc.Authentication.PersistentCookie");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(bool autoLogout = false)
        {
            return View(new IndexModel { AutoLogout = autoLogout });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(IndexModel model, string returnUrl)
        {
            model.AutoLogout = false;
            _logger.Debug(this, "Checking that ModelState is valid.");
            if (ModelState.IsValid)
            {
                _logger.DebugFormat(this, "Validating user with username '{0}'.", model.Username);
                if (_userService.ValidateUser(model.Username, model.Password))
                {
                    var user = _userService.GetUserByUsername(model.Username);
                    _formsAuthenticationService.SignIn(model.Username, _persistentCookie);





                    return RedirectToAction("AddCustomer", "Customer", null);
                    //return RedirectUser(returnUrl, model.Username);
                }
                else
                {
                    _logger.DebugFormat(this, "Password is incorrect for username '{0}'.", model.Username);

                    //Invalid username or password, so show them the error.
                    ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
                }
            }

            return View(model);
        }
    }
}