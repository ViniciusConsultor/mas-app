using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Core;

namespace Shipping.Logging
{
    public class Log4NetLogger : ILogger
    {
        public Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private ILog GetLogger(Type source)
        {
            return BuildLogger(log4net.LogManager.GetLogger(source));
        }

        protected virtual ILog BuildLogger(ILog iLog)
        {
            return new Log4NetLog(iLog.Logger);
        }

        public string GetFormattedExceptionMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return GetFormattedExceptionMessage(ex.InnerException);
            }

            return ex.Message;

        }

        public void Debug(object source, object message)
        {
            GetLogger(source.GetType()).Debug(message);
        }

        public void Info(object source, object message)
        {
            GetLogger(source.GetType()).Info(message);
        }

        public void Warn(object source, object message)
        {
            GetLogger(source.GetType()).Warn(message);
        }

        public void Error(object source, object message)
        {
            GetLogger(source.GetType()).Error(message);
        }

        public void Fatal(object source, object message)
        {
            GetLogger(source.GetType()).Fatal(message);
        }

        public void Debug(object source, object message, Exception ex)
        {
            GetLogger(source.GetType()).Debug(message, ex);
        }

        public void Info(object source, object message, Exception ex)
        {
            GetLogger(source.GetType()).Info(message, ex);
        }

        public void Warn(object source, object message, Exception ex)
        {
            GetLogger(source.GetType()).Warn(message, ex);
        }

        public void Error(object source, object message, Exception ex)
        {
            GetLogger(source.GetType()).Error(message, ex);
        }

        public void Fatal(object source, object message, Exception ex)
        {
            GetLogger(source.GetType()).Fatal(message, ex);
        }

        public void DebugFormat(object source, string format, params object[] args)
        {
            GetLogger(source.GetType()).DebugFormat(format, args);
        }

        public void InfoFormat(object source, string format, params object[] args)
        {
            GetLogger(source.GetType()).InfoFormat(format, args);
        }

        public void WarnFormat(object source, string format, params object[] args)
        {
            GetLogger(source.GetType()).WarnFormat(format, args);
        }

        public void ErrorFormat(object source, string format, params object[] args)
        {
            GetLogger(source.GetType()).ErrorFormat(format, args);
        }

        public void FatalFormat(object source, string format, params object[] args)
        {
            GetLogger(source.GetType()).FatalFormat(format, args);
        }

        public void DebugFormat(Type sourceType, string format, params object[] args)
        {
            GetLogger(sourceType).DebugFormat(format, args);
        }

        public void InfoFormat(Type sourceType, string format, params object[] args)
        {
            GetLogger(sourceType).InfoFormat(format, args);
        }

        public void WarnFormat(Type sourceType, string format, params object[] args)
        {
            GetLogger(sourceType).WarnFormat(format, args);
        }

        public void ErrorFormat(Type sourceType, string format, params object[] args)
        {
            GetLogger(sourceType).ErrorFormat(format, args);
        }

        public void FatalFormat(Type sourceType, string format, params object[] args)
        {
            GetLogger(sourceType).FatalFormat(format, args);
        }
    }
}
