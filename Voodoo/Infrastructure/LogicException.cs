using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;

namespace Voodoo.Infrastructure
{
    public class LogicException : Exception
    {
        public LogicException(string message) : base(message)
        {
        }

        public LogicException(string message, Exception ex) : base(message, ex)
        {
        }

        public List<KeyValuePair> Details { get; set; }
    }
}