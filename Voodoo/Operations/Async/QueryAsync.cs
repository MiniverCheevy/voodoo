using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;

#if net45 || net451

namespace Voodoo.Operations.Async
{
    public abstract class QueryAsync<TRequest, TResponse> : ExecutorAsync<TRequest, TResponse>
        where TResponse : class, IResponse, new() where TRequest : class
    {
        protected QueryAsync(TRequest request) : base(request)
        {
        }
    }
}

#endif