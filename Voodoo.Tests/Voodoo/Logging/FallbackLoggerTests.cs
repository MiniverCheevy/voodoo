using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Logging;

namespace Voodoo.Tests.Voodoo.Logging
{
    [TestClass]
    public class FallbackLoggerTests
    {
        [TestMethod]
        public void Log_IOException_DoesNotBubbleUpException()
        {
            var logger = new FallbackLogger();
            logger.Log("test", @"Q:\askdjf\");
        }
    }
}