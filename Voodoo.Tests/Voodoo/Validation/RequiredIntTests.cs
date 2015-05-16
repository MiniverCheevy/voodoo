using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    
    public class RequiredIntTests
    {
        [Fact]
        public void IsValid_NullableValueIsNull_ReturnsFalse()
        {
            var item = new ClassWithNullableInt();
            var isvalid = item.Validate();
            Assert.Equal(false, isvalid);
        }


        [Fact]
        public void IsValid_NullableValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithNullableInt();
            item.Number = 1;
            var isvalid = item.Validate();
            Assert.Equal(true, isvalid);
        }

        [Fact]
        public void IsValid_NullableValueIsZero_ReturnsTrue()
        {
            var item = new ClassWithNullableInt();
            item.Number = 0;
            var isvalid = item.Validate();
            Assert.Equal(true, isvalid);
        }


        [Fact]
        public void IsValid_ValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithInt();
            item.Number = 1;
            var isvalid = item.Validate();
            Assert.Equal(true, isvalid);
        }

        [Fact]
        public void IsValid_ValueIsLessThanZero_ReturnsTrue()
        {
            var item = new ClassWithInt();
            item.Number = 1;
            var isvalid = item.Validate();
            Assert.Equal(true, isvalid);
        }

        [Fact]
        public void IsValid_ValueIsZero_ReturnsTrue()
        {
            var item = new ClassWithInt();
            item.Number = 0;
            var isvalid = item.Validate();
            Assert.Equal(true, isvalid);
        }
    }
}