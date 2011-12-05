using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Configuration;
using VisitaJayaPerkasa.Web.Security;
using System.Web.Mvc;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class Controller : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private readonly IUserService _userService;
        private readonly IConfigurationManager _configurationManager;
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public Controller() { }

        public Controller(IUserService userService, IFormsAuthenticationService formsAuthenticationService, IConfigurationManager configurationManager)
        {
            _userService = userService;
            _configurationManager = configurationManager;
            _formsAuthenticationService = formsAuthenticationService;
        }

        protected ActionResult RedirectUser(string returnUrl, string username)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                if (_formsAuthenticationService.RequireSSL)
                {
                    if (returnUrl.ToLowerInvariant().StartsWith("http://"))
                    {
                        returnUrl = "https://" + returnUrl.Substring(7);
                    }
                }

                //Redirect them back to where they came from
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect(_formsAuthenticationService.DefaultUrl);
            }
        }
    }
}