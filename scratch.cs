using System;
using System.Collections.Generic;

namespace SolutionItems
{
    public class scratch
    {
        public void foo()
        {
            var request = new ClassToStringify
            {
                AChar = (char)0,
                AGuid = Guid.Parse("537da78a-8b8d-4479-96af-a36c7e9b41af"),
                AString = "String",
                Boolean = true,
                Date = new DateTime(1970, 1, 1, 0, 0, 0),
                DateTimeOffset = DateTimeOffset.Parse("3/18/2019 5:34:03 AM -05:00"),
                Decimal = 1.1m,
                NullableBoolean = false,
                NullableInt = 7,
                Number = 7,
                TestEnum = TestEnum.Blue,
                AnObnoxiousObjectWhosePropertiesThrowExceptions = new TwitchyObject
                {
                },
                AnotherNestedObject = new ClassToStringify
                {
                    AChar = (char)0,
                    AGuid = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    AString = null,
                    Boolean = false,
                    Date = new DateTime(1, 1, 1, 0, 0, 0),
                    DateTimeOffset = DateTimeOffset.Parse("1/1/0001 12:00:00 AM +00:00"),
                    Decimal = 2.2m,
                    NullableBoolean = null,
                    NullableInt = null,
                    Number = 99,
                    TestEnum = (TestEnum)0,
                    AnObnoxiousObjectWhosePropertiesThrowExceptions = null,
                    AnotherNestedObject = null,
                    NestedObject = null,
                    NullObject = null,
                    Items = new List<String> {
               "goo",
               "goo",
               "gachoo",
          },
                    ListOfObjects = null,
                    NestedLists = new List<List<String>> {
 new List<String> {
                    "blue",
               },
          },
                },
                //NestedObject = new ClassToStringify() <-- bidirectional reference found
                NullObject = null,
                Items = new List<String> {
          "foo",
          "bar",
     },
                ListOfObjects = new List<ClassToStringify> {
 new ClassToStringify {
               AChar=(char)0,
               AGuid=Guid.Parse("00000000-0000-0000-0000-000000000000"),
               AString="First",
               Boolean=false,
               Date=new DateTime(2019, 3, 18, 5, 34, 3),
               DateTimeOffset=DateTimeOffset.Parse("1/1/0001 12:00:00 AM +00:00"),
               Decimal=0m,
               NullableBoolean=null,
               NullableInt=null,
               Number=0,
               TestEnum=(TestEnum)0,
               AnObnoxiousObjectWhosePropertiesThrowExceptions=null,
               AnotherNestedObject=null,
               NestedObject=null,
               NullObject=null,
               Items=null,
               ListOfObjects=null,
               NestedLists=null,
          },
 new ClassToStringify {
               AChar=(char)0,
               AGuid=Guid.Parse("00000000-0000-0000-0000-000000000000"),
               AString="Second",
               Boolean=false,
               Date=new DateTime(2018, 3, 18, 5, 34, 3),
               DateTimeOffset=DateTimeOffset.Parse("1/1/0001 12:00:00 AM +00:00"),
               Decimal=0m,
               NullableBoolean=null,
               NullableInt=null,
               Number=0,
               TestEnum=(TestEnum)0,
               AnObnoxiousObjectWhosePropertiesThrowExceptions=null,
               AnotherNestedObject=null,
               NestedObject=null,
               NullObject=null,
               Items=null,
               ListOfObjects=null,
               NestedLists=null,
          },
     },
                NestedLists = new List<List<String>> {
 new List<String> {
               "foo",
          },
     },
            };
        }

    }
}
