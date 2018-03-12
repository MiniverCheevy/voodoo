using System.Threading.Tasks;
using Voodoo.Messages;
using System.Transactions;

namespace Voodoo.Operations.Async
{
    /// <summary>
    /// Because commands use explicit transaction management where allowed you should not call a command from a command
    /// unless you're planning on using DTC.  Another way to approach this is use a command for the outer operation and
    /// Executor`T for the inner operations
    /// </summary>
    public abstract class CommandAsync<TRequest, TResponse> : ExecutorAsync<TRequest, TResponse>
        where TResponse : class, IResponse, new() where TRequest : class
    {
        protected CommandAsync(TRequest request) : base(request)
        {
        }

        public override async Task<TResponse> ExecuteAsync()
        {
            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};

            using (
                var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                    TransactionScopeAsyncFlowOption.Enabled))
            {
                response = await base.ExecuteAsync();
                if (response.IsOk)
                {
                    transaction.Complete();
                }
                return response;
            }
        }
    }
}