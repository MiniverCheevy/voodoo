using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Messages
{
    public interface IKeyValuePair
    {
        string Key { get; set; }
        string Value { get; set; }
    }
}