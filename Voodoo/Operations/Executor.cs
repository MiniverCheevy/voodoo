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
            finally
            {
                try
                {
                    Finally();
                }
                catch (Exception finalException)
                {
                    response.Details.Add(new KeyValuePair("Exception In Final", finalException.ToString()));
                }
            }
            return response;
        }

        protected virtual void Validate()
        {
            ValidateObjectAndThrow(request);
        }

        protected void ValidateObjectAndThrow(object @object)
        {
            var validator = new DataAnnotationsValidator(@object);
            if (validator.IsValid) return;
            var firstMessage = validator.ValidationResultsAsNameValuePair.First();
            throw new LogicException(firstMessage.Value);
        }

        protected virtual void CustomErrorBehavior(Exception ex)
        {
            if (!(ex is LogicException))
            {
                LogManager.Logger.Log(ex);
                var stringResponse = new ObjectStringificationQuery(request).Execute();
                LogManager.Log(stringResponse.IsOk ? stringResponse.Text : stringResponse.Message);
            }
        }

        protected virtual void Finally()
        {
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