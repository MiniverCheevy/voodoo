using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]
    public class GreaterThanZeroIntegerIsRequiredTests
    {
        [TestMethod]
        public void IsValid_NullableValueIsNull_ReturnsFalse()
        {
            var item = new TestClasses.ClassWithNullableInt();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }


        [TestMethod]
        public void IsValid_NullableValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new TestClasses.ClassWithNullableInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_NullableValueIsZero_ReturnsFalse()
        {
            var item = new TestClasses.ClassWithNullableInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }


        [TestMethod]
        public void IsValid_ValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new TestClasses.ClassWithInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_ValueIsZero_ReturnsFalse()
        {
            var item = new TestClasses.ClassWithInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }
    }
}