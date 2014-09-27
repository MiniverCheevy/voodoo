using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableInt
    {
        [GreaterThanZeroIntegerIsRequired]
        public int? Number { get; set; }
    }
}