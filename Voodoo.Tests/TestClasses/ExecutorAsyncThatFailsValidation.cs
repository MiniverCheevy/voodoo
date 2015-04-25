using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Infrastructure;
using Voodoo.Messages;
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
            throw new LogicException("Boom");
        }

        protected override async Task<TestResponse> ProcessRequestAsync()
        {
            
            response.ExecuteFinished = true;
            return response;
        }
    }
}