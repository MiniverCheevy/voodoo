using Voodoo.Messages;
using System.Transactions;

namespace Voodoo.Operations
{


    public abstract class Command<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        public Command(TRequest request):base(request)
        {
            
        }
    }

    /// <summary>
    /// Because commands use explicit transaction management where allowed you should not call a command from a command
    /// unless you're planning on using DTC.  Another way to approach this is use a command for the outer operation and
    /// Executor`T for the inner operations
    /// </summary>
    public abstract class CommandWithExplicitTransaction<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        protected CommandWithExplicitTransaction(TRequest request) : base(request)
        {
        }

        public override TResponse Execute()
        {
            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                response = base.Execute();
                if (response.IsOk)
                {
                    transaction.Complete();
                }
            }
            return response;
        }
    }
}