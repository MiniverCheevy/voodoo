using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voodoo.Tests.Voodoo.Logging
{
    public class TestException : Exception
    {
        public const string UsefulDebugInformation = "This should be logged.";
        public string AllTheInformationYouNeed { get; set; } = UsefulDebugInformation;

        public TestException()
        {
        }

        public TestException(string message) : base(message)
        {
        }
    }
}