using System.Collections.Generic;
#if !PCL
using Voodoo.Validation;
#endif
namespace Voodoo.Tests.TestClasses
{
    public class ClassWithCollection
    {
#if !PCL
        [CollectionMustHaveAtLeastOneItem]
#endif
        public List<string> Items { get; set; }

        public ClassWithCollection()
        {
            Items = new List<string>();
        }
    }
}