﻿using FluentAssertions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Xunit;


namespace Voodoo.Tests.Voodoo
{
    
    public class NameValuePairExtensionTests
    {
        private const string name = "name";
        private const string value = "value";

        private IList<INameValuePair> getList()
        {
            return new List<INameValuePair>
            {
                new NameValuePair {Name = "foo", Value = "bar"},
                new NameValuePair {Name = "green", Value = "red"},
                new NameValuePair {Name = "day", Value = "night"}
            };
        }


        [Fact]
        public void Add_ValidRequest_IsAdded()
        {
            var list = new List<INameValuePair>();
            list.Add(name, value);
            Assert.Single(list);
            Assert.True(list.ContainsName(name));
            Assert.True(list.ContainsValue(value));
        }

        [Fact]
        public void SetValue_Value_ValueIsSet()
        {
            const string newValue = "newValue";
            var list = new List<INameValuePair> {new NameValuePair(name, value)};
            list.SetValue(name, newValue);
            Assert.Single(list);
            Assert.True(list.ContainsName(name));
            Assert.False(list.ContainsValue(value));
            Assert.True(list.ContainsValue(newValue));
        }

        [Fact]
        public void RemoveByName_Value_ValueIsRemoved()
        {
            var list = new List<INameValuePair> {new NameValuePair(name, value)};
            list.RemoveByName(name);
            Assert.Empty(list);
        }

        [Fact]
        public void RemoveByValue_Value_ValueIsRemoved()
        {
            var item = new NameValuePair(name, value);
            var list = new List<INameValuePair> {item, item};
            list.RemoveByValue(value);
            Assert.Empty(list);
        }

        [Fact]
        public void GetValue_Value_ValueIsReturned()
        {
            var item = new NameValuePair(name, value);
            var list = new List<INameValuePair> {item, item};
            var returnedValue = list.GetValue(name);
            Assert.Equal(value, returnedValue);
        }

        [Fact]
        public void Contains_Value_ValueIsReturned()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            Assert.True(list.ContainsItem(name, value));
        }

        [Fact]
        public void Contains_ValueNotPresent_ValueIsNotReturned()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            Assert.False(list.ContainsItem(name, "green"));
        }

        [Fact]
        public void Without_Value_ReturnedWithoutValue()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            var newList = list.Without(name);
            Assert.Equal(3, newList.Count);
            Assert.True(!newList.ContainsName(name));
            Assert.True(!newList.ContainsValue(value));
        }

        [Fact]
        public void ToINameValuePairList_ValueIsDictionary_ReturnesList()
        {
            var items = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"}
            };
            var list = items.ToINameValuePairList();
            Assert.Equal(2, list.Count);
            Assert.True(list.ContainsName("1"));
            Assert.True(list.ContainsName("2"));
            Assert.True(list.ContainsValue("A"));
            Assert.True(list.ContainsValue("B"));
        }

        [Fact]
        public void ToINameValuePairList_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameValuePairList();
            Debug.WriteLine(list.Count());
            list.Count().Should().Be(3);
            Assert.True(list.ContainsName(TestEnum.Red.ToString()));
            Assert.True(list.ContainsName(TestEnum.Blue.ToString()));
            Assert.True(list.ContainsName("Red Orange Yellow"));
            Assert.True(list.ContainsValue("1"));
            Assert.True(list.ContainsValue("2"));
            Assert.True(list.ContainsValue("3"));
        }

        [Fact]
        public void ToINameValuePairListWithUnfriendlyNames_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameValuePairListWithUnfriendlyNames();
            Assert.Equal(3, list.Count);
            Assert.True(list.ContainsName(TestEnum.Red.ToString()));
            Assert.True(list.ContainsName(TestEnum.Blue.ToString()));
            Assert.True(list.ContainsName(TestEnum.RedOrangeYellow.ToString()));
            Assert.True(list.ContainsValue("1"));
            Assert.True(list.ContainsValue("2"));
            Assert.True(list.ContainsValue("3"));
        }

        [Fact]
        public void ToDictionary_ExistingList_IsOk()
        {
            var list = typeof(TestEnum).ToINameValuePairListWithUnfriendlyNames();
            var dictionary = list.ToDictionary();
        }


        [Fact]
        public void ToINameValuePairList_ValueIsEnumWithDescriptionAndDisplayAttributes_ReturnesList()
        {
            var list = typeof(TestEnumWithDescriptionAndDisplay).ToINameValuePairList();
            Assert.Equal(3, list.Count);
            Assert.True(list.ContainsName("Crimson"));
            Assert.True(list.ContainsName("Azure"));
            Assert.True(list.ContainsName("Red Orange Yellow"));
            Assert.True(list.ContainsValue("1"));
            Assert.True(list.ContainsValue("2"));
            Assert.True(list.ContainsValue("3"));
        }

        [Fact]
        public void ToINameValuePairList_ValueIsEnumWithDescription_ReturnesList()
        {
            var list = typeof(MeasurementType).ToINameValuePairList();
            Assert.Equal(4, list.Count);
            Assert.True(list.ContainsName("Numerator/Denominator - Lowest Common Multiple"));
        }

        [Fact]
        public void AsEnumerable_NameValueCollection_ReturnsList()
        {
            var collection = new NameValueCollection {{name, value}};
            var list = collection.AsEnumerable().ToArray();

            Assert.Single(list);
            Assert.True(list.ContainsName(name));
            Assert.True(list.ContainsValue(value));
        }
    }
}