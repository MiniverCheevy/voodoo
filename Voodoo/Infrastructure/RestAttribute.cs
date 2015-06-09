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
            Roles=new string[] {};
        }
        public RestAttribute(Verb verb, string resource, bool allowAnonymous)
        {
            Verb = verb;
            Resource = resource;
            AllowAnonymous = allowAnonymous;
            Roles = new string[] { };
        }

        public RestAttribute(Verb verb, string resource, params string[] roles)
        {
            Verb = verb;
            Resource = resource;
            AllowAnonymous = false;
            Roles = roles;
        }

        public string[] Roles { get; set; }

        public bool AllowAnonymous { get; set; }

        public Verb Verb { get; set; }
        public string Resource { get; set; }
    }
}