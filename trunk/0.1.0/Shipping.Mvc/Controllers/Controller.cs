using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Shipping.Configuration;
using Shipping.Logging;
using Shipping.Business.Services;
using Shipping.Web.Security;

namespace Shipping.Mvc.Controllers
{
    public class Controller : Shipping.Web.Mvc.Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IConfigurationManager _configurationManager;
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public Controller(ILogger logger, IUserService userService, IFormsAuthenticationService formsAuthenticationService, IConfigurationManager configurationManager)
        {
            _logger = logger;
            _userService = userService;
            _configurationManager = configurationManager;
            _formsAuthenticationService = formsAuthenticationService;
        }

        protected ActionResult RedirectUser(string returnUrl, string username)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                _logger.Debug(this, "Checking if require SSL is set on the cookie before redirecting.");

                if (_formsAuthenticationService.RequireSSL)
                {
                    if (returnUrl.ToLowerInvariant().StartsWith("http://"))
                    {
                        _logger.DebugFormat(this, "Return url '{0}' is not secure, so changing the scheme.", returnUrl);

                        returnUrl = "https://" + returnUrl.Substring(7);
                    }
                }

                _logger.DebugFormat(this, "Return url '{0}' is not empty, so redirecting.", returnUrl);

                //Redirect them back to where they came from
                return Redirect(returnUrl);
            }
            else
            {
                _logger.DebugFormat(this, "Redirecting to default url '{0}'.", _formsAuthenticationService.DefaultUrl);
                return Redirect(_formsAuthenticationService.DefaultUrl);                 
            }
        }
    }
}