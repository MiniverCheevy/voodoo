using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Tests.TestClasses
{
    public class ClassToStringify
    {
        public int Number { get; set; }
        public decimal Decimal { get; set; }
        public List<string> Items { get; set; }
        public ClassToStringify NullObject { get; set; }
        public ClassToStringify NestedObject { get; set; }
    }
}