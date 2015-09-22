using Voodoo.Messages;
#if !DNXCORE50 && !PCL
using System.Transactions;
#endif

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
#if !DNXCORE50 && !PCL
            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
#endif
            response = base.Execute();
            if (response.IsOk)
            {
#if !DNXCORE50 && !PCL
                    transaction.Complete();
#endif
            }
#if !DNXCORE50 && !PCL
        }
#endif
            return response;
        }
    }
}