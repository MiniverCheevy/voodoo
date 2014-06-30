using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithRequiredNonZeroNullableInt
    {
        [RequiredNonZeroInt]
        public int? Number { get; set; }
    }
}