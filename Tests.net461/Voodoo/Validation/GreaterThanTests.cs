using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Validation;
using Voodoo.Validation.Infrastructure;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]
    public class GreaterThanTests
    {
        [TestMethod]
        public void EmptyValues_IsValid()
        {
            var entity = new TestClasses.ClassWithDoubleProperties();
            var isValid = entity.IsValid();
            isValid.Should().Be(true);
        }

        [TestMethod]
        public void EmptyDateSource_IsNotValid()
        {
            var entity = new TestClasses.ClassWithDoubleProperties();
            entity.Date2 = "1/1/1970".To<DateTime>();
            var isValid = entity.IsValid();
            isValid.Should().Be(true);
        }

        [TestMethod]
        public void EmptyDateTarget_IsNotValid()
        {
            var entity = new TestClasses.ClassWithDoubleProperties();
            entity.Date1 = "1/1/1970".To<DateTime>();
            var isValid = entity.IsValid();
            isValid.Should().Be(true);
        }

        [TestMethod]
        public void IsNotGreaterThanDate_IsNotValid()
        {
            var entity = new TestClasses.ClassWithDoubleProperties();
            entity.Date1 = "1/1/1970".To<DateTime>();
            entity.Date2 = "1/1/1960".To<DateTime>();
            var isValid = entity.IsValid();
            isValid.Should().Be(false);
        }

        [TestMethod]
        public void IsNotGreaterThanInt_IsNotValid()
        {
            var entity = new TestClasses.ClassWithDoubleProperties();
            entity.Int1 = 10;
            entity.Int2 = 1;
            var isValid = entity.IsValid();
            isValid.Should().Be(false);
        }

        [TestMethod]
        public void IsNotGreaterThanDecimal_IsNotValid()
        {
            var entity = new TestClasses.ClassWithDoubleProperties();
            entity.Decimal1 = 10;
            entity.Decimal2 = 1;
            var isValid = entity.IsValid();
            isValid.Should().Be(false);
        }
    }
}