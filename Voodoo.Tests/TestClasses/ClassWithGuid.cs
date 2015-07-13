using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithGuid
    {
#if (!PCL)
        [RequiredGuid]
#endif
        public Guid Guid { get; set; }
    }
}