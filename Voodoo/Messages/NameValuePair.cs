using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Messages
{
    public class NameValuePair : INameValuePair
    {
        public NameValuePair()
        {
        }

        public NameValuePair(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        [Obsolete("Use Name instead.")]
        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        public override string ToString()
        {
            return string.Format("NameValuePair Name:{0} Value:{1}", Name.To<string>(), Value.To<string>());
        }
    }
}