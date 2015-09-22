using System;
using System.Collections.Generic;
using Voodoo.Infrastructure.Notations;

namespace Voodoo.Tests.TestClasses
{
    public class ClassToStringify
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Decimal { get; set; }
        public List<string> Items { get; set; }
        public List<List<string>> NestedLists { get; set; }
        public ClassToStringify NullObject { get; set; }
        public ClassToStringify NestedObject { get; set; }
        public ClassToStringify AnotherNestedObject { get; set; }
        public List<ClassToStringify> ListOfObjects { get; set; }
        public string AString { get; set; }
        public TwitchyObject AnObnoxiousObjectWhosePropertiesThrowExceptions { get; set; }
        public TestEnum TestEnum { get; set; }
        public bool Boolean { get; set; }
        public bool? NullableBoolean { get; set; }
        public int? NullableInt { get; set; }

        [Secret]
        public string SecretProperty { get; set; }

        public ClassToStringify()
        {
            SecretProperty = "Shhhh!";
        }
    }
}