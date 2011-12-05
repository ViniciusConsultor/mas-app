using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;
using System.Net.Security;
using VisitaJayaPerkasa.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace VisitaJayaPerkasa.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                  "Error", // Route name
                  "Error/{action}", // URL with parameters
                  new { controller = "Error", action = "Index" } // Parameter defaults
              );

            routes.MapRoute(
                "Debug", // Route name
                "Debug/{action}", // URL with parameters
                new { controller = "Debug", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "OnBoarding", // Route name
                "OnBoarding/{action}", // URL with parameters
                new { controller = "OnBoarding", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Email", // Route name
                "Email/{action}", // URL with parameters
                new { controller = "Email", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
               "Password", // Route name
               "Password/{action}/{username}", // URL with parameters
               new { controller = "Password", action = "Index", username = UrlParameter.Optional } // Parameter defaults
           );

            routes.MapRoute(
               "Portal", // Route name
               "Portal/{controller}/{action}/{id}", // URL with parameters
               new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
           );

            routes.MapRoute(
                "LogIn", // Route name
                "{action}/{username}", // URL with parameters
                new { controller = "LogIn", action = "LogOn", username = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["VisitaJayaPerkasa.Mvc.AllowInvalidSslCertificates"]))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                });
            }
            var container = Bootstrapper.GetStructureMapContainer();

            GlobalFilters.Filters.Add(container.GetInstance<PopulateControllerContextAttribute>());

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            Bootstrapper.Bootstrap();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }

        void Session_Start(object sender, EventArgs e)
        {
            //Added in order to force the same session to stay alive.
            Session["KeepAlive"] = "KeepAlive";
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            string url = null;

            try
            {
                if (Request != null && Request.Url != null)
                {
                    url = Request.Url.ToString();
                }
            }
            catch
            {
                url = null;
            }

            try
            {

            }
            catch
            {
                throw ex;
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //Allows Return Urls to be a full patch instead of a relative path.
            string redirectUrl = this.Response.RedirectLocation;

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                this.Response.RedirectLocation = Regex.Replace(redirectUrl, "ReturnUrl=(?'url'[^&]*)", delegate(Match m)
                {
                    string url = HttpUtility.UrlDecode(m.Groups["url"].Value);

                    Uri u = new Uri(this.Request.Url, url);

                    return string.Format("ReturnUrl={0}", HttpUtility.UrlEncode(u.ToString()));

                }, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            }
        }
    }
}