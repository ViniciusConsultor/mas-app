using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Net;

using log4net;
using log4net.Core;

namespace Shipping.Logging
{
    public class Log4NetLog : log4net.Core.LogImpl
    {
        private readonly static Type _declaringType = typeof(Log4NetLog);
        private static long _count = 1;
        private static long _countRecycleLimit = 1000000000000000L;
        private static object _lock = new object();

        public Log4NetLog(log4net.Core.ILogger logger)
            : base(logger)
        { }

        protected virtual void SetEventProperties(LoggingEvent loggingEvent)
        {
            loggingEvent.Properties["version"] = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            loggingEvent.Properties["host"] = Dns.GetHostName();

            lock (_lock)
            {
                if (_count > _countRecycleLimit)
                {
                    _count = 1;
                }

                loggingEvent.Properties["sequence"] = _count++.ToString();
            }
        }

        public override void Debug(object message)
        {
            Debug(message, null);
        }

        public override void DebugFormat(string format, params object[] args)
        {
            Debug(string.Format(format, args), null);
        }

        public override void Debug(object message, Exception exception)
        {
            LoggingEvent loggingEvent = new LoggingEvent(_declaringType, Logger.Repository, Logger.Name, Level.Debug, message, exception);
            SetEventProperties(loggingEvent);
            Logger.Log(loggingEvent);
        }

        public override void Info(object message)
        {
            Info(message, null);
        }

        public override void InfoFormat(string format, params object[] args)
        {
            Info(string.Format(format, args), null);
        }

        public override void Info(object message, Exception exception)
        {
            LoggingEvent loggingEvent = new LoggingEvent(_declaringType, Logger.Repository, Logger.Name, Level.Info, message, exception);
            SetEventProperties(loggingEvent);
            Logger.Log(loggingEvent);
        }

        public override void Warn(object message)
        {
            Warn(message, null);
        }

        public override void WarnFormat(string format, params object[] args)
        {
            Warn(string.Format(format, args), null);
        }

        public override void Warn(object message, Exception exception)
        {
            LoggingEvent loggingEvent = new LoggingEvent(_declaringType, Logger.Repository, Logger.Name, Level.Warn, message, exception);
            SetEventProperties(loggingEvent);
            Logger.Log(loggingEvent);
        }

        public override void Error(object message)
        {
            Error(message, null);
        }

        public override void ErrorFormat(string format, params object[] args)
        {
            Error(string.Format(format, args), null);
        }

        public override void Error(object message, Exception exception)
        {
            LoggingEvent loggingEvent = new LoggingEvent(_declaringType, Logger.Repository, Logger.Name, Level.Error, message, exception);
            SetEventProperties(loggingEvent);
            Logger.Log(loggingEvent);
        }

        public override void Fatal(object message)
        {
            Fatal(message, null);
        }

        public override void FatalFormat(string format, params object[] args)
        {
            Fatal(string.Format(format, args), null);
        }

        public override void Fatal(object message, Exception exception)
        {
            LoggingEvent loggingEvent = new LoggingEvent(_declaringType, Logger.Repository, Logger.Name, Level.Fatal, message, exception);
            SetEventProperties(loggingEvent);
            Logger.Log(loggingEvent);
        }
    }
}
