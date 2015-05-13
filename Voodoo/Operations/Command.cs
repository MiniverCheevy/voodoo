using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#if !DNXCORE50
using System.Transactions;
#endif
using Voodoo.Messages;

namespace Voodoo.Operations
{
    public abstract class Command<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        protected Command(TRequest request) : base(request)
        {
        }

        
        public override TResponse Execute()
        {
#if !DNXCORE50
            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
#endif
                response = base.Execute();
            if (response.IsOk)
            {
#if !DNXCORE50
                    transaction.Complete();
#endif
            }
#if !DNXCORE50
            }
#endif
                return response;
        }
    }
}