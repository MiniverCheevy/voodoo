using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Infrastructure;

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
            Details = new List<KeyValuePair>();
        }

        public int NumberOfRowsEffected { get; set; }

        public bool IsOk { get; set; }
        public string Message { get; set; }

        public IList<KeyValuePair> Details { get; set; }

        public Exception Exception { get; set; }

        public void SetExceptions(Exception ex)
        {
            IsOk = false;
            var logicException = ex as LogicException;
            if (logicException != null)
            {
                Details = logicException.Details;
                if (logicException.InnerException != null)
                    Exception = logicException.InnerException;
            }

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            Message = ex.Message;
            Exception = ex;
        }
    }
}