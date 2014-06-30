using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Messages
{
    public class KeyValuePair : IKeyValuePair
    {
        public KeyValuePair()
        {
        }

        public KeyValuePair(string name, string value)
        {
            Key = name;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}