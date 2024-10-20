using Xunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using FluentAssertions;

namespace Voodoo.Tests.Voodoo.Operations
{
    
    public class ObjectEmissionTests
    {
        [Fact]
        public void SmokeTest()
        {
            var request = new ObjectEmissionRequest { Source = GetValidRequest() };
            var response = new ObjectEmissionQuery(request).Execute();
            Assert.Null(response.Message);
            Assert.True(response.IsOk);
            Debug.WriteLine(response.Text);
            Assert.True(!response.Text.Contains("SecretProperty"));
            Assert.True(!response.Text.Contains("Secret Secret"));
            Assert.Contains("537da78a-8b8d-4479-96af-a36c7e9b41af", response.Text);
            Assert.DoesNotContain("null", response.Text);
        }
        [Fact]
        public void ListOfObjectsTest()
        {
            var request = new ObjectEmissionRequest { Source = GetSimpleRequest() };
            var response = new ObjectEmissionQuery(request).Execute();
            Assert.Null(response.Message);
            Assert.True(response.IsOk);
            Debug.WriteLine(response.Text);
            Assert.Contains("1234567", response.Text);
            Assert.Contains("ABCDEFG", response.Text);
            Assert.Contains("NESTED", response.Text);
            Assert.Contains("DEEPLY NESTED", response.Text);


        }
        [Fact]
        public void ComparisonTest()
        {
            var request = GetValidRequest();
            var emmited = GetEmmitedRequest();
            //emmited.Should().BeEquivalentTo(request);

            //TODO: Write Custom EquivelentTo Method?
            //Fluent Assertions has issues with circular references
            //And Date precision
        }
        public ClassToStringify GetSimpleRequest()
        {
            return new ClassToStringify
            {
                ListOfObjects = new List<ClassToStringify> {
                    new ClassToStringify{  AString = "ABCDEFG",
                    ListOfObjects = new List<ClassToStringify>
                    {
                        new ClassToStringify{  AString = "NESTED"
                        , ListOfObjects = new List<ClassToStringify>{
                            new ClassToStringify{ AString = "DEEPLY NESTED"}
                        } }                  }
                    },
                    new ClassToStringify{  AString = "1234567"}
                }
            };
        }

        public ClassToStringify GetValidRequest()
        {
            var request = new ClassToStringify
            {
                Decimal = 1.1M,
                Items = new List<string> { "foo", "bar" },
                NestedLists = new List<List<string>> { new List<string> { "foo" } },
                Number = 7,
                Date = "1/1/1970".To<DateTime>(),
                Boolean = true,
                NullableInt = 7,
                NullableBoolean = false,
                AGuid = Guid.Parse("537da78a-8b8d-4479-96af-a36c7e9b41af"),
                DateTimeOffset = DateTimeOffset.Now,
                SecretProperty = "Secret Secret"

            };
            request.NestedObject = request;
            request.AnotherNestedObject = new ClassToStringify
            {
                Decimal = 2.2M,
                Items = new List<string> { "goo", "goo", "gachoo" },
                NestedLists = new List<List<string>> { new List<string> { "blue" } },
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
        public ClassToStringify GetEmmitedRequest()
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

            return request;

        }

    }
}