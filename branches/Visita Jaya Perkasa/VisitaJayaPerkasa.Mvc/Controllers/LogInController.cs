using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Web.Security;
using VisitaJayaPerkasa.Configuration;
using VisitaJayaPerkasa.Web.Mvc;
using VisitaJayaPerkasa.Mvc.Models;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class LogInController : VisitaJayaPerkasa.Mvc.Controllers.Controller
    {
        private IUserService _userService;
        private IFormsAuthenticationService _formsAuthenticationService;
        private IConfigurationManager _configurationManager;
        private bool _persistentCookie = false;

        public LogInController(IUserService userService, IFormsAuthenticationService formsAuthenticationService, IConfigurationManager configurationManager)
            : base(userService, formsAuthenticationService, configurationManager)
        {
            _userService = userService;
            _formsAuthenticationService = formsAuthenticationService;
            _configurationManager = configurationManager;

            //Initialize the persistent cookie setting so that it can be reused.
            _persistentCookie = _configurationManager.GetBoolean("VisitaJayaPerkasa.Login.Mvc.Authentication.PersistentCookie");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOn(bool autoLogout = false)
        {
            return View(new LogInIndexModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOn(LogInIndexModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ValidateUser(model.Username, model.Password))
                {
                    var user = _userService.GetUserByUsername(model.Username);
                    _formsAuthenticationService.SignIn(model.Username, _persistentCookie);

                    //return RedirectToAction("Index", "Customer", null);
                    return RedirectUser(returnUrl, model.Username);
                }
                else
                {
                    //Invalid username or password, so show them the error.
                    ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult LogOff(string returnUrl, bool autoLogOut = false)
        {
            _formsAuthenticationService.SignOut();

            string autoLogOutString = autoLogOut ? "autoLogout=true&" : string.Empty;
            return Redirect(_formsAuthenticationService.LoginUrl + "?" + autoLogOutString + "ReturnUrl=" + HttpUtility.UrlEncode(returnUrl));
        }
    }
}
