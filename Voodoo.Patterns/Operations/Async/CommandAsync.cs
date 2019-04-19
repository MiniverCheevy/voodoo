using System;
using System.Threading.Tasks;
using System.Transactions;
using Voodoo.Messages;

namespace Voodoo.Operations.Async
{

    public abstract class CommandAsync<TRequest, TResponse> : ExecutorAsync<TRequest, TResponse>
        where TResponse : class, IResponse, new() where TRequest : class
    {
        public CommandAsync(TRequest request) : base(request)
        {

        }
    }


    public abstract class CommandWithExplicitTransactionAsync<TRequest, TResponse> : ExecutorAsync<TRequest, TResponse>
    where TResponse : class, IResponse, new() where TRequest : class
    {
        protected CommandWithExplicitTransactionAsync(TRequest request) : base(request)
        {
        }

        public override async Task<TResponse> ExecuteAsync()
        {
            response = new TResponse { IsOk = true };
            try
            {
                var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted };

                using (
                    var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                        TransactionScopeAsyncFlowOption.Enabled))
                {
                    response = await base.ExecuteAsync();
                    if (response.IsOk)
                        transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                response = BuildResponseWithException(ex);
            }
            return response;
        }
    }
}
