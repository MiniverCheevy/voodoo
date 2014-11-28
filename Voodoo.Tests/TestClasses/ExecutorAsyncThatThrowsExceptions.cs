using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Voodoo.Tests.TestClasses
{
    public class ExecutorAsyncThatThrowsExceptions : ExecutorAsync<EmptyRequest,Response>
    {
        public ExecutorAsyncThatThrowsExceptions(EmptyRequest request) : base(request)
        {
        }

        protected override async Task<Response> ProcessRequestAsync()
        {
            await Task.Delay(300);
            throw new Exception("Boom");
        }
    }
}
