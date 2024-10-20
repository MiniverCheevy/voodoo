using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Voodoo.Logging;

namespace Voodoo.Tests.Voodoo.Logging
{
    
    public class FallbackLoggerTests
    {
        [Fact]
        public void Log_IOException_DoesNotBubbleUpException()
        {
            var logger = new FallbackLogger();
            logger.Log("test", @"Q:\askdjf\");
        }
    }
}