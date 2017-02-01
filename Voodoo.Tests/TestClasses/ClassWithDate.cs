using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
#if !DNX46
    [Serializable]
#endif
    public class ClassWithDate
    {
#if (!PCL)
        [RequiredDateTime]
#endif
        public DateTime DateAndTime { get; set; }
    }
}