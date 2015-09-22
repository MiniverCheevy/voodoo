using System;
using System.Collections.Generic;
using Voodoo.Helpers;

namespace Voodoo.Messages
{
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }


    public class Response : IResponse
    {
        public Response()
        {
            IsOk = true;
            Details = new List<INameValuePair>();
        }

        public int NumberOfRowsEffected { get; set; }
        public bool IsOk { get; set; }
        public bool HasLogicException { get; set; }
        public string Message { get; set; }
        public IList<INameValuePair> Details { get; set; }
        public Exception Exception { get; set; }

        public void SetExceptions(Exception ex)
        {
            var decorator = new ResponseExceptionDecorator(this, ex);
            decorator.Decorate();
        }

        public void AppendResponse(IResponse response)
        {
            IsOk = IsOk && response.IsOk;
            if (Message == null)
                Message = response.Message;
            else
                Details.Add(new NameValuePair("", response.Message));

            if (response.Details != null)
                response.Details.ForEach(c => Details.Add(c));
        }
    }
}