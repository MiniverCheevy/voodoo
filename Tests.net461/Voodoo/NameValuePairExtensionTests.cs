using FluentAssertions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Voodoo.Tests.Voodoo
{
    [TestClass]
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


        [TestMethod]
        public void Add_ValidRequest_IsAdded()
        {
            var list = new List<INameValuePair>();
            list.Add(name, value);
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(true, list.ContainsName(name));
            Assert.AreEqual(true, list.ContainsValue(value));
        }

        [TestMethod]
        public void SetValue_Value_ValueIsSet()
        {
            const string newValue = "newValue";
            var list = new List<INameValuePair> {new NameValuePair(name, value)};
            list.SetValue(name, newValue);
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(true, list.ContainsName(name));
            Assert.AreEqual(false, list.ContainsValue(value));
            Assert.AreEqual(true, list.ContainsValue(newValue));
        }

        [TestMethod]
        public void RemoveByName_Value_ValueIsRemoved()
        {
            var list = new List<INameValuePair> {new NameValuePair(name, value)};
            list.RemoveByName(name);
            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void RemoveByValue_Value_ValueIsRemoved()
        {
            var item = new NameValuePair(name, value);
            var list = new List<INameValuePair> {item, item};
            list.RemoveByValue(value);
            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void GetValue_Value_ValueIsReturned()
        {
            var item = new NameValuePair(name, value);
            var list = new List<INameValuePair> {item, item};
            var returnedValue = list.GetValue(name);
            Assert.AreEqual(value, returnedValue);
        }

        [TestMethod]
        public void Contains_Value_ValueIsReturned()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            Assert.AreEqual(true, list.ContainsItem(name, value));
        }

        [TestMethod]
        public void Contains_ValueNotPresent_ValueIsNotReturned()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            Assert.AreEqual(false, list.ContainsItem(name, "green"));
        }

        [TestMethod]
        public void Without_Value_ReturnedWithoutValue()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            var newList = list.Without(name);
            Assert.AreEqual(3, newList.Count);
            Assert.AreEqual(true, !newList.ContainsName(name));
            Assert.AreEqual(true, !newList.ContainsValue(value));
        }

        [TestMethod]
        public void ToINameValuePairList_ValueIsDictionary_ReturnesList()
        {
            var items = new Dictionary<int, string>
            {
                {1, "A"},
                {2, "B"}
            };
            var list = items.ToINameValuePairList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(true, list.ContainsName("1"));
            Assert.AreEqual(true, list.ContainsName("2"));
            Assert.AreEqual(true, list.ContainsValue("A"));
            Assert.AreEqual(true, list.ContainsValue("B"));
        }

        [TestMethod]
        public void ToINameValuePairList_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameValuePairList();
            Debug.WriteLine(list.Count());
            list.Count().Should().Be(3);
            Assert.AreEqual(true, list.ContainsName(TestEnum.Red.ToString()));
            Assert.AreEqual(true, list.ContainsName(TestEnum.Blue.ToString()));
            Assert.AreEqual(true, list.ContainsName("Red Orange Yellow"));
            Assert.AreEqual(true, list.ContainsValue("1"));
            Assert.AreEqual(true, list.ContainsValue("2"));
            Assert.AreEqual(true, list.ContainsValue("3"));
        }

        [TestMethod]
        public void ToINameValuePairListWithUnfriendlyNames_ValueIsEnum_ReturnesList()
        {
            var list = typeof(TestEnum).ToINameValuePairListWithUnfriendlyNames();
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(true, list.ContainsName(TestEnum.Red.ToString()));
            Assert.AreEqual(true, list.ContainsName(TestEnum.Blue.ToString()));
            Assert.AreEqual(true, list.ContainsName(TestEnum.RedOrangeYellow.ToString()));
            Assert.AreEqual(true, list.ContainsValue("1"));
            Assert.AreEqual(true, list.ContainsValue("2"));
            Assert.AreEqual(true, list.ContainsValue("3"));
        }

        [TestMethod]
        public void ToDictionary_ExistingList_IsOk()
        {
            var list = typeof(TestEnum).ToINameValuePairListWithUnfriendlyNames();
            var dictionary = list.ToDictionary();
        }


        [TestMethod]
        public void ToINameValuePairList_ValueIsEnumWithDescriptionAndDisplayAttributes_ReturnesList()
        {
            var list = typeof(TestEnumWithDescriptionAndDisplay).ToINameValuePairList();
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(true, list.ContainsName("Crimson"));
            Assert.AreEqual(true, list.ContainsName("Azure"));
            Assert.AreEqual(true, list.ContainsName("Red Orange Yellow"));
            Assert.AreEqual(true, list.ContainsValue("1"));
            Assert.AreEqual(true, list.ContainsValue("2"));
            Assert.AreEqual(true, list.ContainsValue("3"));
        }

        [TestMethod]
        public void ToINameValuePairList_ValueIsEnumWithDescription_ReturnesList()
        {
            var list = typeof(MeasurementType).ToINameValuePairList();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(true, list.ContainsName("Numerator/Denominator - Lowest Common Multiple"));
        }

        [TestMethod]
        public void AsEnumerable_NameValueCollection_ReturnsList()
        {
            var collection = new NameValueCollection {{name, value}};
            var list = collection.AsEnumerable().ToArray();

            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(true, list.ContainsName(name));
            Assert.AreEqual(true, list.ContainsValue(value));
        }
    }
}