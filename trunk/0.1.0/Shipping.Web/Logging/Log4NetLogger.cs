using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Web.Logging
{
    public class Log4NetLogger : Shipping.Logging.Log4NetLogger
    {
        protected override log4net.ILog BuildLogger(log4net.ILog iLog)
        {
            return new Log4NetLog(iLog.Logger);
        }
    }
}
