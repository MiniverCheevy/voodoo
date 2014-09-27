using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Voodoo.Tests.TestClasses
{
   

    public class ClassToStringify
    {
        public int Number { get; set; }
        public decimal Decimal { get; set; }
        public List<string> Items { get; set; }
        public List<List<string>> NestedLists { get; set; }
        public ClassToStringify NullObject { get; set; }
        public ClassToStringify NestedObject { get; set; }
        public string AString { get; set; }
        public TwitchyObject AnObnoxiousObjectWhosePropertiesThrowExceptions { get; set; }
    }
}