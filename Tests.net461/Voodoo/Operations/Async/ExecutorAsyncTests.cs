using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Xunit;


namespace Voodoo.Tests.Voodoo.Operations.Async
{
    
    public class ExecutorTests
    {
        [Fact]
        public async Task Execute_ThrowsException_IsNotOk()
        {
            var response = await new ExecutorAsyncThatThrowsExceptions(new EmptyRequest()).ExecuteAsync();
            Assert.Equal("Boom", response.Message);
            Assert.False(response.IsOk);
            Assert.False(response.ExecuteFinished);
        }

        [Fact]
        public async Task Execute_FailsValidation_IsNotOk()
        {
            var response = await new ExecutorAsyncThatFailsValidation(new EmptyRequest()).ExecuteAsync();
            Assert.Equal("Boom", response.Message);
            Assert.False(response.IsOk);
            Assert.False(response.ExecuteFinished);
        }
    }
}