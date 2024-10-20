using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    
    public class RequiredDateTimeTests
    {
        [Fact]
        public void IsValid_NullableDateNull_ReturnsFalse()
        {
            var item = new ClassWithNullableDate();
            var isValid = item.IsValid();
            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_NullableDateIsMaxValue_ReturnsFalse()
        {
            var item = new ClassWithNullableDate();
            item.DateAndTime = DateTime.MaxValue;
            var isValid = item.IsValid();
            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_NullableDateIsMinValue_ReturnsFalse()
        {
            var item = new ClassWithNullableDate();
            item.DateAndTime = DateTime.MinValue;
            var isValid = item.IsValid();
            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_NullableDateIsValidValue_ReturnsTrue()
        {
            var item = new ClassWithNullableDate();
            item.DateAndTime = "1/1/1970".To<DateTime>();
            var isValid = item.IsValid();
            Assert.True(isValid);
        }
    }
}