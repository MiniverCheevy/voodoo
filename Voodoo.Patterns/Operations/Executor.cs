using System;
using Voodoo.Messages;
using Voodoo.Validation.Infrastructure;

namespace Voodoo.Operations
{
    public abstract class Executor<TRequest, TResponse> where TRequest : class where TResponse : class, IResponse, new()
    {
        protected TRequest request;
        protected TResponse response;

        protected Executor(TRequest request)
        {
            this.request = request;
        }

        public virtual TResponse Execute()
        {
            response = new TResponse {IsOk = true};

            try
            {
                Validate();
                response = ProcessRequest();
            }
            catch (Exception ex)
            {
                response = BuildResponseWithException(ex);
            }

            return response;
        }

        protected virtual void Validate()
        {
            ValidationManager.Validate(request);
        }

        protected virtual void CustomErrorBehavior(Exception ex)
        {
           
        }

        protected TResponse BuildResponseWithException(Exception ex)
        {
            response = new TResponse {IsOk = false};
            
            CustomErrorBehavior(ex);
            response.SetExceptions(ex);
            if (VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging)
                response.Exception = null;
            return response;
        }

        protected abstract TResponse ProcessRequest();
    }
}