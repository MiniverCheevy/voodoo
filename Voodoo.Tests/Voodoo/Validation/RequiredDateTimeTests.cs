using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]

    public class RequiredDateTimeTests
    {
        [TestMethod]
        public void IsValid_NullableDateIsNull_ReturnsFalse()
        {
            var item = new ClassWithNullableDate();
            var isValid = item.IsValid();
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValid_NullableDateIsMaxValue_ReturnsFalse()
        {
            var item = new ClassWithNullableDate();
            item.DateAndTime = DateTime.MaxValue;
            var isValid = item.IsValid();
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValid_NullableDateIsMinValue_ReturnsFalse()
        {
            var item = new ClassWithNullableDate();
            item.DateAndTime = DateTime.MinValue;
            var isValid = item.IsValid();
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValid_NullableDateIsValidValue_ReturnsTrue()
        {
            var item = new ClassWithNullableDate();
            item.DateAndTime = "1/1/1970".To<DateTime>();
            var isValid = item.IsValid();
            Assert.AreEqual(true, isValid);
        }
    }
}