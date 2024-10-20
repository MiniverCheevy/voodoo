using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Tests.TestClasses;
using Xunit;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    
    public class RequiredIntTests
    {
        [Fact]
        public void IsValid_NullableValueNull_ReturnsFalse()
        {
            var item = new ClassWithRequiredNullableInt();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }


        [Fact]
        public void IsValid_NullableValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredNullableInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }

        [Fact]
        public void IsValid_NullableValueIsZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredNullableInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }


        [Fact]
        public void IsValid_ValueIsGreaterThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }

        [Fact]
        public void IsValid_ValueIsLessThanZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredInt();
            item.Number = 1;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }

        [Fact]
        public void IsValid_ValueIsZero_ReturnsTrue()
        {
            var item = new ClassWithRequiredInt();
            item.Number = 0;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }
    }
}