using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableGuid
    {
        [RequiredGuid]
        public Guid? Guid { get; set; }
    }
}