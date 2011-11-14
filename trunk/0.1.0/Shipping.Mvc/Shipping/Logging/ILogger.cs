using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Logging
{
    public interface ILogger
    {
        void Debug(object source, object message);
        void Info(object source, object message);
        void Warn(object source, object message);
        void Error(object source, object message);
        void Fatal(object source, object message);
        void Debug(object source, object message, Exception ex);
        void Info(object source, object message, Exception ex);
        void Warn(object source, object message, Exception ex);
        void Error(object source, object message, Exception ex);
        void Fatal(object source, object message, Exception ex);
        void DebugFormat(object source, string format, params object[] args);
        void InfoFormat(object source, string format, params object[] args);
        void WarnFormat(object source, string format, params object[] args);
        void ErrorFormat(object source, string format, params object[] args);
        void FatalFormat(object source, string format, params object[] args);
        void DebugFormat(Type sourceType, string format, params object[] args);
        void InfoFormat(Type sourceType, string format, params object[] args);
        void WarnFormat(Type sourceType, string format, params object[] args);
        void ErrorFormat(Type sourceType, string format, params object[] args);
        void FatalFormat(Type sourceType, string format, params object[] args);
        string GetFormattedExceptionMessage(Exception ex);
    }
}
