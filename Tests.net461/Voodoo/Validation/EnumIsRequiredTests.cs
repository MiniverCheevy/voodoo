using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;

namespace Voodoo.Tests.Voodoo.Validation
{
    
    public class EnumIsRequiredTests
    {
        [Fact]
        public void IsValid_ValueIsNotSet_ReturnsFalse()
        {
            var item = new ClassWithEnum();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }

        [Fact]
        public void IsValid_ValueNull_ReturnsFalse()
        {
            var item = new ClassWithNullableEnum();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }

        [Fact]
        public void IsValid_ValueIsValid_ReturnsTrue()
        {
            var item = new ClassWithEnum();
            item.TestEnum = TestEnum.Blue;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }

        [Fact]
        public void IsValid_NullableValueIsValid_ReturnsTrue()
        {
            var item = new ClassWithNullableEnum();
            item.TestEnum = TestEnum.Blue;
            var isvalid = item.IsValid();
            Assert.True(isvalid);
        }

        [Fact]
        public void IsValid_WrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }

        [Fact]
        public void IsValid_NullableWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }

        [Fact]
        public void IsValid_NullWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }

        [Fact]
        public void IsValid_NotNullWrongPropertyType_ReturnsFalse()
        {
            var item = new ClassMismatchedEnumIsRequiredAttribute();
            item.SomeProperty = new ClassMismatchedEnumIsRequiredAttribute();
            var isvalid = item.IsValid();
            Assert.False(isvalid);
        }
    }
}