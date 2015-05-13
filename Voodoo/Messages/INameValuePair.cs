using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Messages
{
    public interface INameValuePair
    {
        string Name { get; set; }
        string Value { get; set; }

    }
}