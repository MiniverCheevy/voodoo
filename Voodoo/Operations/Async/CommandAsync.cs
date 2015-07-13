using System.Threading.Tasks;
using Voodoo.Messages;
#if !DNXCORE50 && !PCL
using System.Transactions;
#endif

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
#if !DNXCORE50 && !PCL


            var transactionOptions = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};


            using (
                var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                    TransactionScopeAsyncFlowOption.Enabled))
            {
#endif
            response = await base.ExecuteAsync();
            if (response.IsOk)
            {
#if !DNXCORE50 && !PCL
                    transaction.Complete();
#endif
            }
            return response;
        }

#if !DNXCORE50 && !PCL
        }
#endif
    }
}

#endif