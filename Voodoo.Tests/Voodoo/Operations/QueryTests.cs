using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void Execute_ExceptionIsThrown_IsNotOk()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            var result = new QueryThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(false, result.IsOk);
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [TestMethod]
        public void Execute_ErrorAndMergedResponses_IsNotOk()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            var result = new QueryThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.AreEqual(false, result.IsOk);
            var response = new Response {IsOk = true};
            response.AppendResponse(result);
            Assert.AreEqual(false, response.IsOk);

            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [TestMethod]
        public void Execute_ExceptionIsThrown_ExceptionIsBubbled()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
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