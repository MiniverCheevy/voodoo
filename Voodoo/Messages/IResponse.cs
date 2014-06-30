using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Messages
{
    public interface IResponse
    {
        bool IsOk { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
        IList<KeyValuePair> Details { get; set; }
        void SetExceptions(Exception ex);
    }
}