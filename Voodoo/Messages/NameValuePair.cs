using System;

namespace Voodoo.Messages
{
    public class NameValuePair : INameValuePair
    {
        public string Name { get; set; }
        public string Value { get; set; }

        [Obsolete("Use Name instead.")]
        public string Key
        {
            get { return Name; }
            set { Name = value; }
        }

        public NameValuePair()
        {
        }

        public NameValuePair(string name, string value)
        {
            Name = name;
            Value = value;
        }        
    }
}