using System.Collections.Generic;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithCollection
    {
        [CollectionMustHaveAtLeastOneItem]
        public List<string> Items { get; set; }

        public ClassWithCollection()
        {
            Items = new List<string>();
        }
    }
}