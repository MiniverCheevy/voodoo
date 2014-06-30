using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithCollection
    {
        [CollectionMustHaveAtLeastOneItem]
        public List<string> Items { get; set; }
    }
}