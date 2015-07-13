using System;
using System.Collections.Generic;
using System.Diagnostics;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo.Operations
{
    public class ObjectEmissionTests
    {
        [Fact]
        public void SmokeTest()
        {
            var request = GetValidRequest();
            var response = new ObjectEmissionQuery(request).Execute();
            Assert.Equal(null, response.Message);
            Assert.Equal(true, response.IsOk);
            Debug.WriteLine(response.Text);
        }

        public void foo()
        {
            var request = new ClassToStringify
            {
                AString = "String",
                Boolean = true,
                Date = new DateTime(1970, 1, 1, 0, 0, 0),
                Decimal = 1.1m,
                NullableBoolean = false,
                NullableInt = 7,
                Number = 7,
                TestEnum = TestEnum.Blue,
                AnObnoxiousObjectWhosePropertiesThrowExceptions = new TwitchyObject(),
                AnotherNestedObject = new ClassToStringify
                {
                    AString = null,
                    Boolean = false,
                    Date = new DateTime(1, 1, 1, 0, 0, 0),
                    Decimal = 2.2m,
                    NullableBoolean = null,
                    NullableInt = null,
                    Number = 99,
                    TestEnum = 0,
                    AnObnoxiousObjectWhosePropertiesThrowExceptions = null,
                    AnotherNestedObject = null,
                    NestedObject = null,
                    NullObject = null,
                    Items = new List<string>
                    {
                        "goo",
                        "goo",
                        "gachoo"
                    },
                    ListOfObjects = null,
                    NestedLists = new List<List<string>>
                    {
                        new List<string>
                        {
                            "blue"
                        }
                    }
                },
                //NestedObject = new ClassToStringify() <-- bidirectional reference found
                NullObject = null,
                Items = new List<string>
                {
                    "foo",
                    "bar"
                },
                ListOfObjects = new List<ClassToStringify>
                {
                    new ClassToStringify
                    {
                        AString = "First",
                        Boolean = false,
                        Date = new DateTime(2015, 6, 21, 7, 36, 891),
                        Decimal = 0m,
                        NullableBoolean = null,
                        NullableInt = null,
                        Number = 0,
                        TestEnum = 0,
                        AnObnoxiousObjectWhosePropertiesThrowExceptions = null,
                        AnotherNestedObject = null,
                        NestedObject = null,
                        NullObject = null,
                        Items = null,
                        ListOfObjects = null,
                        NestedLists = null
                    },
                    new ClassToStringify
                    {
                        AString = "Second",
                        Boolean = false,
                        Date = new DateTime(2014, 6, 21, 7, 36, 891),
                        Decimal = 0m,
                        NullableBoolean = null,
                        NullableInt = null,
                        Number = 0,
                        TestEnum = 0,
                        AnObnoxiousObjectWhosePropertiesThrowExceptions = null,
                        AnotherNestedObject = null,
                        NestedObject = null,
                        NullObject = null,
                        Items = null,
                        ListOfObjects = null,
                        NestedLists = null
                    }
                },
                NestedLists = new List<List<string>>
                {
                    new List<string>
                    {
                        "foo"
                    }
                }
            };
            ;
        }

        public ClassToStringify GetValidRequest()
        {
            var request = new ClassToStringify
            {
                Decimal = 1.1M,
                Items = new List<string> {"foo", "bar"},
                NestedLists = new List<List<string>> {new List<string> {"foo"}},
                Number = 7,
                Date = "1/1/1970".To<DateTime>(),
                Boolean = true,
                NullableInt = 7,
                NullableBoolean = false
            };
            request.NestedObject = request;
            request.AnotherNestedObject = new ClassToStringify
            {
                Decimal = 2.2M,
                Items = new List<string> {"goo", "goo", "gachoo"},
                NestedLists = new List<List<string>> {new List<string> {"blue"}},
                Number = 99
            };

            request.AString = "String";
            request.AnObnoxiousObjectWhosePropertiesThrowExceptions = new TwitchyObject();
            request.ListOfObjects = new List<ClassToStringify>
            {
                new ClassToStringify {AString = "First", Date = DateTime.Now},
                new ClassToStringify {AString = "Second", Date = DateTime.Now.AddYears(-1)}
            };
            request.TestEnum = TestEnum.Blue;
            return request;
        }
    }
}