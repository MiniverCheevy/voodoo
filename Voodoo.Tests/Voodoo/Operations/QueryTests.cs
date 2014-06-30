using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void Execute_ExceptionIsThrown_IsNotOk()
        {
            var result = new QueryThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(false, result.IsOk);
        }

        [TestMethod]
        public void Execute_ExceptionIsThrown_ExceptionIsBubbled()
        {
            var result = new QueryThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(TestingResponse.OhNo, result.Message);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public void Execute_QueryReturnsResponse_IsOk()
        {
            var result = new QueryThatDoesNotThrowErrors(new EmptyRequest()).Execute();
            Assert.IsNotNull(result);
            Assert.IsNull(result.Message);
            Assert.AreEqual(true, result.IsOk);
        }
    }
}