using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;

namespace Voodoo.Tests.TestClasses
{
    public class Role : LookupValue
    {
        public List<User> Users { get; set; } = new List<User>();
    }
}