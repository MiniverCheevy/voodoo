using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithInt
    {
        [GreaterThanZeroIntegerIsRequired]
        public int Number { get; set; }
    }
}