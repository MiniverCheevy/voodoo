using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassMismatchedEnumIsRequiredAttribute
    {
        [EnumIsRequired]
        public ClassMismatchedEnumIsRequiredAttribute SomeProperty { get; set; }
    }
}