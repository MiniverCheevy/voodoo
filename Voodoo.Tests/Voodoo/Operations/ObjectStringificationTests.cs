using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Operations
{
    [TestClass]
    public class ObjectStringificationTests
    {
        [TestMethod]
        public void SmokeTest()
        {
            var request = GetValidRequest();
            var response = new ObjectStringificationQuery(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Debug.WriteLine(response.Text);
        }

        public ClassToStringify GetValidRequest()
        {
            var response = new ClassToStringify();
            response.Decimal = 1.1M;
            response.Items = new List<string> {"foo", "bar"};
            response.Number = 7;
            response.NestedObject = response;
            return response;
        }
    }
}