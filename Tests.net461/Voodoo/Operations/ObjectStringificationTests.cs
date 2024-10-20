﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo.Operations
{
    
    public class ObjectStringificationTests
    {
        [Fact]
        public void SmokeTest()
        {
            var request = GetValidRequest();
            var response = new ObjectStringificationQuery(request).Execute();
            Assert.Null(response.Message);
            Assert.True(response.IsOk);
            Debug.WriteLine(response.Text);
            Assert.True(!response.Text.Contains("SecretProperty"));
        }

        [Fact]
        public void Execute_ClassWithCollection_DoesNotExceedMaximumItemsInGraph()
        {
            var request = new ClassWithCollection();
            const string @string = "a";
            for (var i = 0; i < 100; ++i)
            {
                request.Items.Add(@string);
            }
            var current = VoodooGlobalConfiguration.LogMaximumNumberOfItemsInCollection;
            VoodooGlobalConfiguration.LogMaximumNumberOfItemsInCollection = 10;
            var response = new ObjectStringificationQuery(request).Execute();
            Assert.Null(response.Message);
            Assert.True(response.IsOk);
            var rows = response.Text.Split((char) 13);
            Assert.Equal(13, rows.Count());
            Debug.WriteLine(response.Text);
            VoodooGlobalConfiguration.LogMaximumNumberOfItemsInCollection = current;
        }

        public ClassToStringify GetValidRequest()
        {
            var response = new ParentClassToStringify
            {
                Decimal = 1.1M,
                Items = new List<string> {"foo", "bar"},
                NestedLists = new List<List<string>> {new List<string> {"foo"}},
                Number = 7
            };
            response.NestedObject = response;
            response.AnotherNestedObject = new ClassToStringify
            {
                Decimal = 2.2M,
                Items = new List<string> {"goo", "goo", "gachoo"},
                NestedLists = new List<List<string>> {new List<string> {"blue"}},
                Number = 99
            };

            response.AString = "String";
            response.AnObnoxiousObjectWhosePropertiesThrowExceptions = new TwitchyObject();
            return response;
        }
    }
}