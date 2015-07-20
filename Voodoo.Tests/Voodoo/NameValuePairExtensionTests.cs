using System.Collections.Generic;
using System.Collections.Specialized;
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
            Assert.Equal(1, list.Count());
            Assert.Equal(true, list.ContainsName(name));
            Assert.Equal(true, list.ContainsValue(value));
        }

        [Fact]
        public void SetValue_Value_ValueIsSet()
        {
            const string newValue = "newValue";
            var list = new List<INameValuePair> {new NameValuePair(name, value)};
            list.SetValue(name, newValue);
            Assert.Equal(1, list.Count());
            Assert.Equal(true, list.ContainsName(name));
            Assert.Equal(false, list.ContainsValue(value));
            Assert.Equal(true, list.ContainsValue(newValue));
        }

        [Fact]
        public void RemoveByName_Value_ValueIsRemoved()
        {
            var list = new List<INameValuePair> {new NameValuePair(name, value)};
            list.RemoveByName(name);
            Assert.Equal(0, list.Count());
        }

        [Fact]
        public void RemoveByValue_Value_ValueIsRemoved()
        {
            var item = new NameValuePair(name, value);
            var list = new List<INameValuePair> {item, item};
            list.RemoveByValue(value);
            Assert.Equal(0, list.Count());
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
            Assert.Equal(true, list.ContainsItem(name, value));
        }

        [Fact]
        public void Contains_ValueNotPresent_ValueIsNotReturned()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            Assert.Equal(false, list.ContainsItem(name, "green"));
        }

        [Fact]
        public void Without_Value_ReturnedWithoutValue()
        {
            var list = getList();
            var test = new NameValuePair(name, value);
            list.Add(test);
            var newList = list.Without(name);
            Assert.Equal(3, newList.Count);
            Assert.Equal(true, !newList.ContainsName(name));
            Assert.Equal(true, !newList.ContainsValue(value));
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
            Assert.Equal(true, list.ContainsName("1"));
            Assert.Equal(true, list.ContainsName("2"));
            Assert.Equal(true, list.ContainsValue("A"));
            Assert.Equal(true, list.ContainsValue("B"));
        }

        [Fact]
        public void ToINameValuePairList_ValueIsEnum_ReturnesList()
        {
            var list = typeof (TestEnum).ToINameValuePairList();
            Assert.Equal(3, list.Count);
            Assert.Equal(true, list.ContainsName(TestEnum.Red.ToString()));
            Assert.Equal(true, list.ContainsName(TestEnum.Blue.ToString()));
            Assert.Equal(true, list.ContainsName("Red Orange Yellow"));
            Assert.Equal(true, list.ContainsValue("1"));
            Assert.Equal(true, list.ContainsValue("2"));
            Assert.Equal(true, list.ContainsValue("3"));
        }

        [Fact]
        public void ToINameValuePairListWithUnfriendlyNames_ValueIsEnum_ReturnesList()
        {
            var list = typeof (TestEnum).ToINameValuePairListWithUnfriendlyNames();
            Assert.Equal(3, list.Count);
            Assert.Equal(true, list.ContainsName(TestEnum.Red.ToString()));
            Assert.Equal(true, list.ContainsName(TestEnum.Blue.ToString()));
            Assert.Equal(true, list.ContainsName(TestEnum.RedOrangeYellow.ToString()));
            Assert.Equal(true, list.ContainsValue("1"));
            Assert.Equal(true, list.ContainsValue("2"));
            Assert.Equal(true, list.ContainsValue("3"));
        }

#if (!PCL  && !DNXCORE50)
        [Fact]
        public void AsEnumerable_NameValueCollection_ReturnsList()
        {
            var collection = new NameValueCollection {{name, value}};
            var list = collection.AsEnumerable().ToArray();
            
            Assert.Equal(1, list.Count());            
            Assert.Equal(true, list.ContainsName(name));
            Assert.Equal(true, list.ContainsValue(value));
            
        }
#endif
    }
}