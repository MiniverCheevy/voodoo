using Voodoo.Messages;

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