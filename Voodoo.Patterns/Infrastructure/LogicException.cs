using System;
using System.Collections.Generic;
using Voodoo.Messages;

namespace Voodoo.Infrastructure
{
    public class LogicException : Exception
    {
        public LogicException(string message) : base(message)
        {
            Details = new List<INameValuePair>();
        }

        public LogicException(string message, Exception ex) : base(message, ex)
        {
            Details = new List<INameValuePair>();
        }

        public List<INameValuePair> Details { get; set; }
    }
}