using System;
using Voodoo.Logging;

namespace Voodoo.Logging
{
    public static class LogManager
    {
        public static LogLevels CurrentLogLevel { get; set; } = LogLevels.Info;
        private static ILogger logger;

        public static ILogger Logger
        {
            get { return logger ?? (logger = getDefaultLogger()); }
            set { logger = value; }
        }

        private static ILogger getDefaultLogger()
        {
            return new FallbackLogger();
        }

        public static void Log(string message, LogLevels level = LogLevels.Info)
        {
            if (level.To<int>() > CurrentLogLevel.To<int>())
                return;

            if (Logger == null)
                Logger = getDefaultLogger();
            Log(message, level.ToString(), level);
        }

        public static void Log(Exception ex, LogLevels level = LogLevels.Error)
        {
            if (level.To<int>() > CurrentLogLevel.To<int>())
                return;

            if (Logger == null)
                Logger = getDefaultLogger();

            Logger.Log(ex);
        }

        public static void Log(string message, string category, LogLevels level = LogLevels.Error)
        {
            if (level.To<int>() > CurrentLogLevel.To<int>())
                return;

            Logger.Log(message, category);
        }
    }
}