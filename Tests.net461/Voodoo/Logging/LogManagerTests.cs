using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Voodoo.Logging;

namespace Voodoo.Tests.Voodoo.Logging
{
    
    public class LogManagerTests
    {
        [Fact]
        public void LogLevelIsEqual_MessageIsLogged()
        {
            var testLogger = new TestLogger();
            LogManager.Logger = testLogger;
            LogManager.CurrentLogLevel = LogLevels.Info;
            LogManager.Log("test", LogLevels.Info);
            testLogger.Messages.Count.Should().Be(1);
        }

        [Fact]
        public void LogLevelIsLower_MessageIsLogged()
        {
            var testLogger = new TestLogger();
            LogManager.Logger = testLogger;
            LogManager.CurrentLogLevel = LogLevels.Info;
            LogManager.Log("test", LogLevels.Critical);
            testLogger.Messages.Count.Should().Be(1);
        }

        [Fact]
        public void LogLevelIsHiger_MessageIsNotLogged()
        {
            var testLogger = new TestLogger();
            LogManager.Logger = testLogger;
            LogManager.CurrentLogLevel = LogLevels.Info;
            LogManager.Log("test", LogLevels.Trace);
            testLogger.Messages.Count.Should().Be(0);
        }
    }
}