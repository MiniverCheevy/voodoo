using System;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Validation.Infrastructure;

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
            await Task.Run(() =>
                {
                    ValidationManager.Validate(request);
                    return Task.FromResult(0);
                }
            );
        }

        protected virtual void CustomErrorBehavior(Exception ex)
        {
        }

        protected TResponse BuildResponseWithException(Exception ex)
        {
            response = new TResponse {IsOk = false};
            CustomErrorBehavior(ex);
            ExceptionHelper.HandleException(ex, GetType(), request);
            response.SetExceptions(ex);
            if (VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging)
                response.Exception = null;
            return response;
        }
    }
}