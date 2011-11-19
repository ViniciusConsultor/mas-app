using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Shipping.Web.Logging
{
    public class Log4NetLog : Shipping.Logging.Log4NetLog
    {
        public Log4NetLog(log4net.Core.ILogger logger)
            : base(logger)
        { }

        protected override void SetEventProperties(log4net.Core.LoggingEvent loggingEvent)
        {
            base.SetEventProperties(loggingEvent);

            HttpContext currentContext = HttpContext.Current;

            if (currentContext != null)
            {
                try
                {
                    if (currentContext.Request != null && currentContext.Request.Url != null)
                    {
                        loggingEvent.Properties["url"] = currentContext.Request.Url.ToString();
                    }
                }
                catch { }

                try
                {
                    if (currentContext.Session != null)
                    {
                        loggingEvent.Properties["sessionid"] = currentContext.Session.SessionID;
                    }
                }
                catch { }

                try
                {
                    string username = null;

                    if (currentContext.User.Identity != null && currentContext.User.Identity.IsAuthenticated)
                    {
                        username = currentContext.User.Identity.Name;
                    }
                    loggingEvent.Properties["username"] = username;
                }
                catch { }

                try
                {
                    string userAgent = null;

                    if (currentContext.Request != null)
                    {
                        userAgent = currentContext.Request.UserAgent;
                    }
                    loggingEvent.Properties["user-agent"] = userAgent;
                }
                catch { }

                try
                {
                    string userHostAddress = null;

                    if (currentContext.Request != null)
                    {
                        userHostAddress = currentContext.Request.UserHostAddress;
                    }
                    loggingEvent.Properties["user-ip"] = userHostAddress;
                }
                catch { }
            }
        }
    }
}
