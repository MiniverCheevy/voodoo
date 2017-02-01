using System;
using System.Collections.Generic;

namespace Voodoo.Tests.TestClasses
{
#if !DNX46
    [Serializable]
#endif
    public class ClassToReflect
    {
        public ClassWithDate ComplexObject { get; set; }
        public int? NullableInt { get; set; }
        public int Int { get; set; }
        public string String { get; set; }
        public DateTime DateAndTime { get; set; }
        public DateTime? NullableDateAndTime { get; set; }
        public TestEnum TestEnum { get; set; }
        public decimal Decimal { get; set; }

        public void Method(string @string, int @int, int? nullableInt, List<string> list)
        {
        }
    }

    public class ClassWithAncestor : ClassToReflect
    {
    }

}