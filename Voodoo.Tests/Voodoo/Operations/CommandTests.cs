using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation.Infrastructure;
using Message=Voodoo.Validation.Infrastructure.Messages;
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
            Assert.AreEqual(Message.ValidationErrorsOccured, result.Message);
            Assert.AreNotEqual(true, result.IsOk);

        }
    }
}