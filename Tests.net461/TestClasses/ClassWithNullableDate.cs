using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    public class ClassWithNullableDate
    {
        [RequiredDateTime]
        public DateTime? DateAndTime { get; set; }
    }
}