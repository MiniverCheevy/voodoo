using System.Threading.Tasks;
using Voodoo.Infrastructure;
using Voodoo.Messages;
#if !NET40
using Voodoo.Operations.Async;

namespace Voodoo.Tests.TestClasses
{
    public class ExecutorAsyncThatFailsValidation : ExecutorAsync<EmptyRequest, TestResponse>
    {
        public ExecutorAsyncThatFailsValidation(EmptyRequest request)
            : base(request)
        {
        }

        protected override async Task Validate()
        {
            await Task.Run(() => { throw new LogicException("Boom"); });
        }

        protected override async Task<TestResponse> ProcessRequestAsync()
        {
            await Task.Run(() =>
            {
                response.ExecuteFinished = true;
            });
            return response;
        }
    }
}
#endif