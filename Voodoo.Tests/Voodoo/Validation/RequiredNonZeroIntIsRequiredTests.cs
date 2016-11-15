using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    
    public class RequiredNonZeroIntIsRequiredTests
    {
        [Fact]
        public void IsValid_NullableValueIsNull_ReturnsFalse()
        {
            var item = new ClassWithRequiredNonZeroNullableInt();
            var isvalid = item.IsValid();
            Assert.Equal(false, isvalid);
        }


        [Fact]
        public void IsValid_NullableValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredNonZeroNullableInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.Equal(true, isvalid);
        }

        [Fact]
        public void IsValid_NullableValueIsZero_ReturnsFalse()
        {
            var item = new ClassWithRequiredNonZeroNullableInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.Equal(false, isvalid);
        }


        [Fact]
        public void IsValid_ValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredNonZeroInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.Equal(true, isvalid);
        }

        [Fact]
        public void IsValid_ValueIsLessThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredNonZeroInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.Equal(true, isvalid);
        }

        [Fact]
        public void IsValid_ValueIsZero_ReturnsFalse()
        {
            var item = new ClassWithRequiredNonZeroInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.Equal(false, isvalid);
        }
    }
}