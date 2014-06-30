using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute
    {
        [CollectionMustHaveAtLeastOneItem]
        public ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute SomeProperty { get; set; }
    }
}