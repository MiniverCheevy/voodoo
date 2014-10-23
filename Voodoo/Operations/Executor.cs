using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;
using Voodoo.Validation;

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
            if (request == null)
                throw new LogicException("Request is required.");
            ValidateObjectAndThrow(request);
        }

        protected void ValidateObjectAndThrow(object @object)
        {
            var validator = new DataAnnotationsValidator(@object);
            if (validator.IsValid) return;
            var firstMessage = validator.ValidationResultsAsNameValuePair.First();
            var exception = new  LogicException(firstMessage.Value);
			exception.Details = validator.ValidationResultsAsNameValuePair;
			throw exception;
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
            response = new TResponse {IsOk = false};
            response.SetExceptions(ex);
            CustomErrorBehavior(ex);
            return response;
        }

        protected abstract TResponse ProcessRequest();
    }
}