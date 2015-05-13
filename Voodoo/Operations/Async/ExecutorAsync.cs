using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Validation.Infrastructure;

#if ! DNX40 

namespace Voodoo.Operations.Async
{
    public abstract class ExecutorAsync<TRequest, TResponse> where TRequest : class
        where TResponse : class, IResponse, new()
    {
        protected TRequest request;
        protected TResponse response;

        protected ExecutorAsync(TRequest request)
        {
            this.request = request;
        }

        
        public virtual async Task<TResponse> ExecuteAsync()
        {
            response = new TResponse {IsOk = true};

            try
            {
                await Validate();
                response = await ProcessRequestAsync();
            }
            catch (Exception ex)
            {
                response = BuildResponseWithException(ex);
            }

            return response;
        }

        protected abstract Task<TResponse> ProcessRequestAsync();

        protected virtual async Task Validate()
        {
            ValidationManager.Validate(request);
        }

        protected virtual async Task CustomErrorBehavior(Exception ex)
        {
            ExceptionHelper.HandleException(ex, GetType(), request);
            if (VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging)
                response.Exception = null;
        }

        protected TResponse BuildResponseWithException(Exception ex)
        {
            response = new TResponse {IsOk = false};
            response.SetExceptions(ex);
            CustomErrorBehavior(ex);
            return response;
        }
    }
}

#endif