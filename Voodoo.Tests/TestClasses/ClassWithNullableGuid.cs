using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableGuid
    {
#if (!PCL)
        [RequiredGuid]
#endif
        public Guid? Guid { get; set; }
    }
}