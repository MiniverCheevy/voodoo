using Voodoo.Messages;
using System.Transactions;
using System;

namespace Voodoo.Operations
{


    public abstract class Command<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        public Command(TRequest request):base(request)
        {
            
        }
    }
    
    public abstract class CommandWithExplicitTransaction<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        protected CommandWithExplicitTransaction(TRequest request) : base(request)
        {
        }

        public override TResponse Execute()
        {
            response = new TResponse { IsOk = true };

            try
            {
                Validate();
                var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted };
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    response = base.Execute();
                    if (response.IsOk)
                    {
                        response = ProcessRequest();
                    }
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