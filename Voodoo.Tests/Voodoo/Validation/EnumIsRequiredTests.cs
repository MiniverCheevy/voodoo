using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]
    public class EnumIsRequiredTests
    {
        [TestMethod] 
        public void IsValid_ValueIsNotSet_ReturnsFalse()
        {
            var item = new ClassWithEnum();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_ValueIsNull_ReturnsFalse()
        {
            var item = new ClassWithNullableEnum();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_ValueIsValid_ReturnsTrue()
        {
            var item = new ClassWithEnum();
            item.TestEnum = TestEnum.Blue;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_NullableValueIsValid_ReturnsTrue()
        {
            var item = new ClassWithNullableEnum();
            item.TestEnum = TestEnum.Blue;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_WrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_NullableWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_NullWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }

        [TestMethod]
        public void IsValid_NotNullWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            item.SomeProperty = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }
    }
}