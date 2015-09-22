using System;

namespace Voodoo.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception ex);
    }

    public interface IDetailedLogger
    {
    }
}