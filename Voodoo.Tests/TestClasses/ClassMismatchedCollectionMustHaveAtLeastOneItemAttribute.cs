using System;
using System.Collections.Generic;
using System.Linq;
#if !PCL
using Voodoo.Validation;
#endif
namespace Voodoo.Tests.TestClasses
{
    public class ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute
    {
#if !PCL
        [CollectionMustHaveAtLeastOneItem]
#endif
        public ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute SomeProperty { get; set; }
    }
}