using Voodoo.Messages;
#if !NETCOREAPP1_0 && !PCL
using System.Transactions;
#endif

namespace Voodoo.Operations
{
/// <summary>
/// Because commands use explicit transaction management where allowed you should not call a command from a command
/// unless you're planning on using DTC.  Another way to approach this is use a command for the outer operation and
/// Executor`T for the inner operations
/// </summary>
    public abstract class Command<TRequest, TResponse> : Executor<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        protected Command(TRequest request) : base(request)
        {
        }

        public override TResponse Execute()
        {
#if !NETCOREAPP1_0 && !PCL
            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
#endif
            response = base.Execute();
            if (response.IsOk)
            {
#if !NETCOREAPP1_0 && !PCL
                    transaction.Complete();
#endif
            }
#if !NETCOREAPP1_0 && !PCL
        }
#endif
            return response;
        }
    }
}