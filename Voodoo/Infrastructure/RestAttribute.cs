using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Infrastructure
{
    public enum Verb
    {
        Get = 0,
        Post = 1,
        Put = 2,
        Delete = 3
    }

    public class RestAttribute : Attribute
    {
        public RestAttribute(Verb verb, string resource)
        {
            Verb = verb;
            Resource = resource;
        }

        public Verb Verb { get; set; }
        public string Resource { get; set; }
    }
}