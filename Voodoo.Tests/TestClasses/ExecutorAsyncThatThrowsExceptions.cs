using System;
using System.Threading.Tasks;
using Voodoo.Messages;
#if !NET40
using Voodoo.Operations.Async;

namespace Voodoo.Tests.TestClasses
{
    public class ExecutorAsyncThatThrowsExceptions : ExecutorAsync<EmptyRequest, TestResponse>
    {
        public ExecutorAsyncThatThrowsExceptions(EmptyRequest request) : base(request)
        {
        }

        protected override async Task<TestResponse> ProcessRequestAsync()
        {
            await Task.Delay(300);
            throw new Exception("Boom");
#pragma warning disable 162
            response.ExecuteFinished = true;
#pragma warning restore 162
        }
    }
}
#endif