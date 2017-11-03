using System;

namespace Voodoo.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception ex);

        void Log(string message, string category);
    }

    public interface IDetailedLogger
    {
    }
}