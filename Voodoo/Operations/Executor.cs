using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Operations.Async;
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

        [DebuggerNonUserCode]
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
            ExceptionHelper.HandleException(ex, this.GetType(), request);
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

        protected abstract TResponse ProcessRequest();
    }
}