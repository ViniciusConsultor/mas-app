using System;
using System.Web.Mvc;

using Shipping.Business.Services;

namespace Shipping.Web.Mvc
{
    /// <summary>
    /// Represents an attribute that is used to populate the logged on user's information.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PopulateControllerContextAttribute : ActionFilterAttribute
    {
        private IUserService _userService;

        public PopulateControllerContextAttribute(IUserService userService)
        {
            _userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_userService == null)
            {
                throw new Exception("IUserService has not been initialized.");
            }

            Context context = new Context();

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                context.User = _userService.GetUserByUsername(filterContext.HttpContext.User.Identity.Name);
            }

            Shipping.Web.Mvc.Controller controller = filterContext.Controller as Shipping.Web.Mvc.Controller;

            if (controller != null)
            {
                controller.Context = context;
            }
        }
    }
}
