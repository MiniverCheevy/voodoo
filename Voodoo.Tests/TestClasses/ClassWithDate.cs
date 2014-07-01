using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Validation;

namespace Voodoo.Tests.TestClasses
{
    [Serializable]
    public class ClassWithDate
    {
        [RequiredDateTime]
        public DateTime DateAndTime { get; set; }
    }
}