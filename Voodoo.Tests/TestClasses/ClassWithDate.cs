using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    #if !DNXCORE50
    [Serializable]
#endif
    public class ClassWithDate
    {
        [RequiredDateTime]
        public DateTime DateAndTime { get; set; }
    }
}