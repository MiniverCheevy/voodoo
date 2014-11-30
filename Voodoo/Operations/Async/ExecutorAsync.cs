using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Validation.Infrastructure;

#if net45 || net451 
namespace Voodoo.Operations.Async
{
    public abstract class ExecutorAsync<TRequest, TResponse> where TRequest : class where TResponse : class, IResponse, new()
    {
        protected TRequest request;
        protected TResponse response;

        protected ExecutorAsync(TRequest request)
        {
            this.request = request;
        }

        [DebuggerNonUserCode]
        public async virtual Task<TResponse> ExecuteAsync()
        {
            response = new TResponse { IsOk = true };

            try
            {
                Validate();
                response = await ProcessRequestAsync();
            }
            catch (Exception ex)
            {
                response = BuildResponseWithException(ex);
            }

            return response;
        }
        protected abstract Task<TResponse> ProcessRequestAsync();
        protected virtual void Validate()
        {
            ValidationManager.Validate(request);
        }

        protected virtual void CustomErrorBehavior(Exception ex)
        {
            if (!(ex is LogicException))
            {
                LogManager.Logger.Log(ex);
                var stringResponse = new ObjectStringificationQuery(request).Execute();
                LogManager.Log(stringResponse.IsOk ? stringResponse.Text : stringResponse.Message);
            }
            if (VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging)
                response.Exception = null;
        }


        protected TResponse BuildResponseWithException(Exception ex)
        {
            response = new TResponse { IsOk = false };
            response.SetExceptions(ex);
            CustomErrorBehavior(ex);
            return response;
        }

        
    }
}
#endif