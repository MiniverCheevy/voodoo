using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]
    public class DataAnnotationsValidatorTests
    {
        [TestMethod]
        public void ClassWithCompare_ValuesDoNotMatch_IsNotOk()
        {
            var @class = new ClassWithCompareValidator() {Password = "abc", ConfirmPassword = "123"};     
            var validator = new DataAnnotationsValidator(@class);
            var response = validator.ValidationResultsAsNameValuePair;
            Assert.AreEqual(false, validator.IsValid);
        }
    }
}
