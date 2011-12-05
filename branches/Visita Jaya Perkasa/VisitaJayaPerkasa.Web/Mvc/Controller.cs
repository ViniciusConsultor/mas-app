using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Web.Mvc
{
    /// <summary>
    /// Represents a base controller for applications.
    /// </summary>
    [RequireHttps]
    [VisitaJayaPerkasa.Web.Mvc.Authorize]
    public class Controller : System.Web.Mvc.Controller
    {
        /// <summary>
        /// Gets or sets the Context for the controller.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public Context Context { get; set; }

        /// <summary>
        /// Gets called after each action is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //For IE so that if won't cache the page.
            if (!(filterContext.Result is FileResult))
            {
                filterContext.HttpContext.Response.CacheControl = "no-cache";
                filterContext.HttpContext.Response.AddHeader("pragma", "no-cache");
            }
            filterContext.HttpContext.Response.Expires = -1;
            base.OnActionExecuted(filterContext);
        }
    }
}
