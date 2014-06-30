using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    public class ClassWithInt
    {
        [RequiredInt]
        public int Number { get; set; }
    }
}