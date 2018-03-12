using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]
    public class RequiredIntTests
    {
        [TestMethod]
        public void IsValid_NullableValueIsNull_ReturnsFalse()
        {
            var item = new ClassWithRequiredNullableInt();
            var isvalid = item.IsValid();
            Assert.AreEqual(false, isvalid);
        }


        [TestMethod]
        public void IsValid_NullableValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredNullableInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_NullableValueIsZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredNullableInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }


        [TestMethod]
        public void IsValid_ValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_ValueIsLessThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }

        [TestMethod]
        public void IsValid_ValueIsZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.AreEqual(true, isvalid);
        }
    }
}