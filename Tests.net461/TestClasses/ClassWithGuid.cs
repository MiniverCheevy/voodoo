using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithGuid
    {
        [RequiredGuid]
        public Guid Guid { get; set; }
    }
}