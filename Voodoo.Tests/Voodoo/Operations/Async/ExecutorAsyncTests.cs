using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Operations.Async
{
    
    public class ExecutorTests
    {
        [Fact]
        public async Task Execute_ThrowsException_IsNotOk()
        {
            
            var response = await new ExecutorAsyncThatThrowsExceptions(new EmptyRequest()).ExecuteAsync();
            Assert.Equal("Boom", response.Message);
            Assert.Equal(false, response.IsOk);
            Assert.Equal(false, response.ExecuteFinished);

        }
        [Fact]
        public async Task Execute_FailsValidation_IsNotOk()
        {

            var response = await new ExecutorAsyncThatFailsValidation(new EmptyRequest()).ExecuteAsync();
            Assert.Equal("Boom", response.Message);
            Assert.Equal(false, response.IsOk);
            Assert.Equal(false, response.ExecuteFinished);

        }
    }
}
