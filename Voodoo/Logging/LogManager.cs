using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Logging
{
    public static class LogManager
    {
        private static ILogger logger;

        public static ILogger Logger
        {
            get { return logger ?? (logger = new FallbackLogger()); }
            set { logger = value; }
        }

        public static void Log(string message)
        {
            Logger.Log(message);
        }

        public static void Log(Exception ex)
        {
            if (Logger == null)
                Logger = new FallbackLogger();

            Logger.Log(ex);
        }
    }
}