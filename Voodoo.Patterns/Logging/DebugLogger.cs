using System;
using System.Diagnostics;
using Voodoo.Logging;

namespace Voodoo.Logging
{

#if !NETCOREAPP1_0

    public class DebugLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }

        public void Log(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        public void Log(string message, string category)
        {
            Debug.WriteLine("****************");
            Debug.WriteLine(category);
            Debug.WriteLine(message);
        }
    }
#endif
}