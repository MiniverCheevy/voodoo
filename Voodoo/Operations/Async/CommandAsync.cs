using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Voodoo.Messages;

#if  net451 
namespace Voodoo.Operations.Async
{
    public abstract class CommandAsync<TRequest, TResponse> : ExecutorAsync<TRequest,TResponse> where TResponse : class, IResponse, new() where TRequest : class
    {
        protected CommandAsync(TRequest request) : base(request)
        {
        }

        public override async Task<TResponse> ExecuteAsync()
        {

            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted };
            

            using (var transaction = 
                new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                response = await base.ExecuteAsync();
                if (response.IsOk)
                    transaction.Complete();
                return response;
            }

        }
    }
}
#endif
