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

        public override string ToString()
        {
            return $"NameValuePair Name:{Name.To<string>()} Value:{Value.To<string>()}";
        }
    }
}