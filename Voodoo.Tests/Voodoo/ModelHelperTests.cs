using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ModelHelperTests
    {
        [TestMethod]
        public void ExtractUserNameFromDomainNameOrEmailAddress_DomainName_IsOk()
        {
            var input = @"DOMAIN\shawn";
            var output = ModelHelper.ExtractUserNameFromDomainNameOrEmailAddress(input);
            output.Should().Be("shawn");
        }
        [TestMethod]
        public void ExtractUserNameFromDomainNameOrEmailAddress_EmailAddress_IsOk()
        {
            var input = @"shawn@shawnland.com";
            var output = ModelHelper.ExtractUserNameFromDomainNameOrEmailAddress(input);
            output.Should().Be("shawn");
        }
        [TestMethod]
        public void ExtractUserNameFromDomainNameOrEmailAddress_RandomString_IsOk()
        {
            var input = @"turtlesallthewaydown";
            var output = ModelHelper.ExtractUserNameFromDomainNameOrEmailAddress(input);
            output.Should().Be("turtlesallthewaydown");
        }
        [TestMethod]
        public void ExtractUserNameFromDomainNameOrEmailAddress_Null_IsOk()
        {
            var input = default(string);
            var output = ModelHelper.ExtractUserNameFromDomainNameOrEmailAddress(input);
            output.Should().Be(string.Empty);
        }

        [TestMethod]
        public void Truncate_SmallerString_IsOk()
        {
            var input = "fish";
            var output = input.Truncate(6);
            output.Should().Be(input);
        }
        [TestMethod]
        public void Truncate_LongerString_IsOk()
        {
            var input = "fishfish";
            var output = input.Truncate(6);
            output.Should().Be("fishfi");
        }
        [TestMethod]
        public void Truncate_Null_IsOk()
        {
            var input = default(string);
            var output = input.Truncate(6);
            output.Should().Be(string.Empty);
        }
        [TestMethod]
        public void FormatPhone_PhoneNumber_IsOk()
        {
            var input = "2252252255";
            var output = ModelHelper.FormatPhone(input);
            output.Should().Be("(225) 225-2255");
        }
        [TestMethod]
        public void FormatPhone_RandomString_IsOk()
        {
            var input = "fishfish";
            var output = ModelHelper.FormatPhone(input);
            output.Should().Be(input);
        }
        [TestMethod]
        public void FormatPhone_Null_IsOk()
        {
            var input = default(string);
            var output = ModelHelper.FormatPhone(input);
            output.Should().Be(string.Empty);
        }
        [TestMethod]
        public void UnFormatPhone_Null_IsOk()
        {
            var input = default(string);
            var output = ModelHelper.UnformatPhone(input);
            output.Should().Be(string.Empty);
        }
        [TestMethod]
        public void UnFormatPhone_FormattedPhone_IsOk()
        {
            var input = "(225) 225-2255";
            var output = ModelHelper.UnformatPhone(input);
            output.Should().Be("2252252255");
        }
#if !PCL && !NETCOREAPP1_0
        [TestMethod]
        public void GetAttributeFromEnumMember_HasAttribute_IsOk()
        {
            var attr = ModelHelper.GetAttributeFromEnumMember<DescriptionAttribute>(MeasurementType.WholeNumber);
            attr.Should().NotBe(null);
            attr.Description.Should().Be("Whole Number");

        }
        [TestMethod]
        public void GetAttributeFromEnumMember_HasNoAttribute_IsOk()
        {
            var attr = ModelHelper.GetAttributeFromEnumMember<DescriptionAttribute>(TestEnum.Blue);
            attr.Should().Be(null);
        }
#endif
    }
}
