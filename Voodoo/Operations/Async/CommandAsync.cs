using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if !DNXCORE50
using System.Transactions;
#endif
using Voodoo.Messages;

#if ! DNX40 && ! DNX45

namespace Voodoo.Operations.Async
{
    public abstract class CommandAsync<TRequest, TResponse> : ExecutorAsync<TRequest, TResponse>
        where TResponse : class, IResponse, new() where TRequest : class
    {
        protected CommandAsync(TRequest request) : base(request)
        {
        }

        public override async Task<TResponse> ExecuteAsync()
        {
#if !DNXCORE50


            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};


            using (
                var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                    TransactionScopeAsyncFlowOption.Enabled))
            {
#endif
                response = await base.ExecuteAsync();
                if (response.IsOk){
#if !DNXCORE50
                    transaction.Complete();
#endif
}
                return response;
            }
#if !DNXCORE50
        }
#endif
    }
}

#endif