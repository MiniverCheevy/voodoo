using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;
using Xunit;

namespace Voodoo.Tests.Voodoo.Validation
{
    
    public class DataAnnotationsValidatorTests
    {
        [Fact]
        public void ClassWithCompare_ValuesDoNotMatch_IsNotOk()
        {
            var @class = new ClassWithCompareValidator() {Password = "abc", ConfirmPassword = "123"};
            var validator = new DataAnnotationsValidator(@class);
            var response = validator.ValidationResultsAsNameValuePair;
            Assert.False(validator.IsValid);
        }
    }
}