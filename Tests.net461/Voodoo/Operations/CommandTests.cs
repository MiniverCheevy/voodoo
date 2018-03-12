using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Logging;
using Voodoo.Tests.Voodoo.Logging;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void Execute_ExceptionIsThrown_IsNotOk()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;

            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(false, result.IsOk);
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [TestMethod]
        public void Execute_ExceptionIsThrown_ExceptionIsBubbled()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(TestingResponse.OhNo, result.Message);
            Assert.IsNotNull(result.Exception);
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [TestMethod]
        public void Execute_CommandReturnsResponse_IsOk()
        {
            var result = new CommandThatDoesNotThrowErrors(new EmptyRequest()).Execute();
            Assert.IsNotNull(result);
            Assert.IsNull(result.Message);
            Assert.AreEqual(true, result.IsOk);
        }


        [TestMethod]
        public void Execute_RequestIsInvalidDataAnnotationsValidatorWithFirstErrorAsMessage_IsNotOk()
        {
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithFirstErrorAsMessage());
            var result = new CommandWithNonEmptyRequest(new RequestWithRequiredString()).Execute();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Message);
            Assert.AreEqual(result.Details.First().Value, result.Message);
            Assert.AreNotEqual(true, result.IsOk);
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithFirstErrorAsMessage());
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithGenericMessage());
        }

        [TestMethod]
        public void Execute_RequestIsInvalidDataAnnotationsValidatorWithGenericMessage_IsNotOk()
        {
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithGenericMessage());
            var result = new CommandWithNonEmptyRequest(new RequestWithRequiredString()).Execute();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Message);
            Assert.AreEqual(Strings.Validation.validationErrorsOccurred, result.Message);
            Assert.AreNotEqual(true, result.IsOk);
        }

        [TestMethod]
        public void Execute_ExceptionIsThrownWithLogInExceptionData_ExtraExceptionPropertiesAreCaptured()
        {
            var testLogger = new TestLogger();
            LogManager.Logger = testLogger;
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            VoodooGlobalConfiguration.ErrorDetailLoggingMethodology = ErrorDetailLoggingMethodology.LogInExceptionData;
            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            testLogger.Exceptions.Count().Should().Be(1);
            var ex = testLogger.Exceptions.First();
            var keys = ex.Data.Keys;
            var values = new List<string>();
            foreach (var key in keys)
            {
                values.Add(ex.Data[key].ToString());
            }
            var hasSpecialPropertyValue = values.Any(c => c.Contains(TestException.UsefulDebugInformation));
            hasSpecialPropertyValue.Should().Be(true);
        }

        [TestMethod]
        public void Execute_ExceptionIsThrownWithLogAsSeperateException_ExtraExceptionPropertiesAreCaptured()
        {
            var testLogger = new TestLogger();
            LogManager.Logger = testLogger;
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            VoodooGlobalConfiguration.ErrorDetailLoggingMethodology = ErrorDetailLoggingMethodology.LogAsSecondException;
            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            testLogger.Exceptions.Count().Should().Be(1);
            testLogger.Messages.Count().Should().Be(1);
            var ex = testLogger.Messages.First();
            ex.IndexOf(TestException.UsefulDebugInformation).Should().BeGreaterThan(0);
        }
    }
}