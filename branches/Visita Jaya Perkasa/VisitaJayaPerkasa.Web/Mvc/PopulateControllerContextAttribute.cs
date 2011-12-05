using System;
using System.Web.Mvc;

using VisitaJayaPerkasa.Business.Services;

namespace VisitaJayaPerkasa.Web.Mvc
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

            VisitaJayaPerkasa.Web.Mvc.Controller controller = filterContext.Controller as VisitaJayaPerkasa.Web.Mvc.Controller;

            if (controller != null)
            {
                controller.Context = context;
            }
        }
    }
}
