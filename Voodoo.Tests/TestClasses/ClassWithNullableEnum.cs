using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableEnum
    {
        [EnumIsRequired]
        public TestEnum? TestEnum { get; set; }
    }
}