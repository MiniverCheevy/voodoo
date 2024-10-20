using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    
    public class GreaterThanZeroIntegerIsRequiredTests
    {
        [Fact]
        public void IsValid_NullableValueIsNull_ReturnsFalse()
        {
            var item = new TestClasses.ClassWithNullableInt();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }


        [Fact]
        public void IsValid_NullableValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new TestClasses.ClassWithNullableInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }

        [Fact]
        public void IsValid_NullableValueIsZero_ReturnsFalse()
        {
            var item = new TestClasses.ClassWithNullableInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }


        [Fact]
        public void IsValid_ValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new TestClasses.ClassWithInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }

        [Fact]
        public void IsValid_ValueIsZero_ReturnsFalse()
        {
            var item = new TestClasses.ClassWithInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }
    }
}