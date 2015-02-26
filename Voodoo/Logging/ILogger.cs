using System;
using System.Collections.Generic;
using System.Linq;

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