using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Logging;

namespace Voodoo.Tests.Voodoo.Logging
{
    public class TestLogger : ILogger
    {
        public List<string> Messages { get; set; } = new List<string>();
        public List<Exception> Exceptions { get; set; } = new List<Exception>();
        public void Log(string message)
        {
            Messages.Add(message);
        }

        public void Log(Exception ex)
        {
            Exceptions.Add(ex);
        }

        public void Log(string message, string category)
        {
            Messages.Add(message);
        }
    }
}