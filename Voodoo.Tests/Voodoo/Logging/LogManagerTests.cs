using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Logging;

namespace Voodoo.Tests.Voodoo.Logging
{
    [TestClass]
    public class LogManagerTests
    {
        [TestMethod]
        public void LogLevelIsEqual_MessageIsLogged()
        {
            var testLogger = new TestLogger();
            LogManager.Logger = testLogger;
            LogManager.CurrentLogLevel = LogLevels.Info;
            LogManager.Log("test", LogLevels.Info);
            testLogger.Messages.Count.Should().Be(1);
        }
        [TestMethod]
        public void LogLevelIsLower_MessageIsLogged()
        {
            var testLogger = new TestLogger();
            LogManager.Logger = testLogger;
            LogManager.CurrentLogLevel = LogLevels.Info;
            LogManager.Log("test", LogLevels.Critical);
            testLogger.Messages.Count.Should().Be(1);
        }
        [TestMethod]
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
