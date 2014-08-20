using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voodoo.Tests.TestClasses
{
    [Serializable]
    public class ClassToReflect
    {
        public ClassWithDate ComplexObject { get; set; }
        public int? NullableInt { get; set; }
        public int Int { get; set; }
        public string String { get; set; }
        public DateTime DateAndTime { get; set; }
        public DateTime? NullableDateAndTime { get; set; }
        public TestEnum TestEnum { get; set; }
        public Decimal Decimal { get; set; }

        public void Method(string @string, int @int, int? nullableInt, List<string> list )
        {
        }
    }
}
