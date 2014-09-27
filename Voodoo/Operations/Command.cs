using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using Voodoo.Messages;

namespace Voodoo.Operations
{
    public abstract class Command<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        protected Command(TRequest request) : base(request)
        {
        }

        [DebuggerNonUserCode]
        public override TResponse Execute()
        {
            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                response = base.Execute();
                if (response.IsOk)
                    transaction.Complete();
                return response;
            }
        }
    }
}