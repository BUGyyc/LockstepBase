using System;

namespace Lockstep.Common.Logging
{

    public static class Log
    {
        public static LogSeverity LogSeverityLevel = LogSeverity.Exception | LogSeverity.Error | LogSeverity.Warn | LogSeverity.Info;

        public static event EventHandler<LogEventArgs> OnMessage;

        public static void SetLogAllSeverities()
        {
            LogSeverityLevel = LogSeverity.Exception | LogSeverity.Error | LogSeverity.Warn | LogSeverity.Info | LogSeverity.Trace;
        }

        public static void Warn(object sender, string message, params object[] args)
        {
            LogMessage(sender, LogSeverity.Warn, message, args);
        }

        public static void Info(object sender, string message, params object[] args)
        {
            LogMessage(sender, LogSeverity.Info, message, args);
        }

        public static void Trace(object sender, string message, params object[] args)
        {
            LogMessage(sender, LogSeverity.Trace, message, args);
        }

        private static void LogMessage(object sender, LogSeverity sev, string format, params object[] args)
        {
            if (Log.OnMessage != null && (LogSeverityLevel & sev) != 0)
            {
                string message = ((args != null && args.Length != 0) ? string.Format(format, args) : format);
                Log.OnMessage(sender, new LogEventArgs(sev, message));
            }
        }
    }
}