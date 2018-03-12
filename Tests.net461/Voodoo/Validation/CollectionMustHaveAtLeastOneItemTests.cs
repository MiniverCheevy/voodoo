using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]
    public class CollectionMustHaveAtLeastOneItemTests
    {
        [TestMethod]
        public void IsValid_ValueIsNotSet_ReturnsFalse()
        {
            var item = new ClassWithCollection();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_ListIsEmpty_ReturnsFalse()
        {
            var item = new ClassWithCollection();
            item.Items = new List<string>();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_ValueIsValid_ReturnsTrue()
        {
            var item = new ClassWithCollection();
            item.Items = new List<string>();
            item.Items.Add("blue");
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_WrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_NullableWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_NullWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_NotNullWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute();
            item.SomeProperty = new ClassMismatchedCollectionMustHaveAtLeastOneItemAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }
    }
}