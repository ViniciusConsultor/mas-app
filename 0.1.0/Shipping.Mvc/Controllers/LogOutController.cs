using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shipping.Web.Security;
using Shipping.Mvc.Models.LogOut;

namespace Shipping.Mvc.Controllers
{
    public class LogOutController : Shipping.Web.Mvc.Controller
    {
        IFormsAuthenticationService _formsAuthenticationService;

        public LogOutController(IFormsAuthenticationService formsAuthenticationService)
        {
            _formsAuthenticationService = formsAuthenticationService;
        }

        /// <summary>
        /// Gets the view that contains the auto logout information
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoLogOut()
        {
            return View(new AutoLogOutModel { Timeout = _formsAuthenticationService.Timeout });
        }

        /// <summary>
        /// Redirects the user back to the login page.
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectToLogIn(string returnUrl, bool autoLogOut = false)
        {
            _formsAuthenticationService.SignOut();

            string autoLogOutString = autoLogOut ? "autoLogout=true&" : string.Empty;
            return Redirect(_formsAuthenticationService.LoginUrl + "?" + autoLogOutString + "ReturnUrl=" + HttpUtility.UrlEncode(returnUrl));
        }

        /// <summary>
        /// Resets a login cookie and session for the user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Reset()
        {
            return null;
        }

    }
}
