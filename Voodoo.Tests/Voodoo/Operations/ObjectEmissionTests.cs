using System;
using System.Collections.Generic;
using System.Diagnostics;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
    public class ObjectEmissionTests
    {
        [TestMethod]
        public void SmokeTest()
        {
            var request = GetValidRequest();
            var response = new ObjectEmissionQuery(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Debug.WriteLine(response.Text);
            Assert.IsTrue(!response.Text.Contains("SecretProperty"));
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