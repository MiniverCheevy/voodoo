using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo.Operations
{
    
    public class QueryTests
    {
        [Fact]
        public void Execute_ExceptionIsThrown_IsNotOk()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            var result = new QueryThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.False(result.IsOk);
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [Fact]
        public void Execute_ErrorAndMergedResponses_IsNotOk()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            var result = new QueryThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.False(result.IsOk);
            var response = new Response {IsOk = true};
            response.AppendResponse(result);
            Assert.False(response.IsOk);

            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [Fact]
        public void Execute_ExceptionIsThrown_ExceptionIsBubbled()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            var result = new QueryThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.Equal(TestingResponse.OhNo, result.Message);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public void Execute_QueryReturnsResponse_IsOk()
        {
            var result = new QueryThatDoesNotThrowErrors(new EmptyRequest()).Execute();
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.True(result.IsOk);
        }
    }
}