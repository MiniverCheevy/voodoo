using System;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{

    [Serializable]

    public class ClassWithDate
    {
#if (!PCL)
        [RequiredDateTime]
#endif
        public DateTime DateAndTime { get; set; }
    }
}