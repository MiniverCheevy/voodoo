﻿using System.Threading.Tasks;
using Voodoo.Messages;
#if !NETCOREAPP1_0 && !PCL
using System.Transactions;
#endif

#if ! NET40 && ! NET45

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
#if !NETCOREAPP1_0 && !PCL


            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};


            using (
                var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                    TransactionScopeAsyncFlowOption.Enabled))
            {
#endif
            response = await base.ExecuteAsync();
            if (response.IsOk)
            {
#if !NETCOREAPP1_0 && !PCL
                    transaction.Complete();
#endif
            }
            return response;
        }

#if !NETCOREAPP1_0 && !PCL
        }
#endif
    }
}

#endif