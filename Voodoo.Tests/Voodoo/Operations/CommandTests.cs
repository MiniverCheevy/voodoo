using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void Execute_ExceptionIsThrown_IsNotOk()
        {
            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(false, result.IsOk);
        }

        [TestMethod]
        public void Execute_ExceptionIsThrown_ExceptionIsBubbled()
        {
            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(TestingResponse.OhNo, result.Message);
            Assert.IsNotNull(result.Exception);
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
        public void Execute_RequestIsInvalid_IsNotOk()
        {
            var result = new CommandWithNonEmptyRequest(new RequestWithRequiredString()).Execute();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Message);
            Assert.AreNotEqual(true, result.IsOk);
        }
    }
}