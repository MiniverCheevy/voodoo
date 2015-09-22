using System;
#if (PCL)
using Voodoo.Logging;
#endif

namespace Voodoo.Logging
{
    public static class LogManager
    {
        private static ILogger logger;

        public static ILogger Logger
        {
            get { return logger ?? (logger = getDefaultLogger()); }
            set { logger = value; }
        }

        private static ILogger getDefaultLogger()
        {
#if PCL
            return new DebugLogger();
#else
            return new FallbackLogger();
#endif
        }

        public static void Log(string message)
        {
            Logger.Log(message);
        }

        public static void Log(Exception ex)
        {
            if (Logger == null)
                Logger = getDefaultLogger();

            Logger.Log(ex);
        }
    }
}