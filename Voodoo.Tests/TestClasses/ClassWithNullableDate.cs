using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableDate
    {
#if (!PCL)
        [RequiredDateTime]
#endif
        public DateTime? DateAndTime { get; set; }
    }
}