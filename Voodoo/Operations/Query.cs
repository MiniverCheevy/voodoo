using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;

namespace Voodoo.Operations
{
    public abstract class Query<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        protected Query(TRequest request) : base(request)
        {
        }
    }
}