using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    public class ClassWithNullableGuid
    {
        [RequiredGuid]
        public Guid? Guid { get; set; }
    }
}