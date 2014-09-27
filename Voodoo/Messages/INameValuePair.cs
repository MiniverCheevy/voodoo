using System;
namespace Voodoo.Messages
{
    public interface INameValuePair
    {
        string Name { get; set; }
        string Value { get; set; }
        [Obsolete("Use name instead")]
        string Key { get; set; }
    }
}