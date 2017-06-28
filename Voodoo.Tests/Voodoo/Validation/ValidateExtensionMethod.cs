using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Validation
{
    [TestClass]
    public class ValidateExtensionMethod
    {
        [TestMethod]
        public void GetValidationResponse_AllErrorsHaveMemberName()
        {
            var request = new MemberDetail
            {
                Id = 0,
                ManagerId = null,
                Name = "Sammy Imel",
                OptionalDate = new DateTime(1931, 6, 6, 0, 0, 0),
                OptionalDecimal = 154.82m,
                OptionalInt = 27774,
                RequiredDate = new DateTime(1936, 6, 11, 0, 0, 0),
                RequiredDecimal = 6731.05m,
                RequiredInt = 11166,
                Title = "expensive Paper Plates",
            };
            var validation = request.GetValidationResponse();
            validation.Details.ForEach(c => Assert.IsTrue(!string.IsNullOrWhiteSpace(c.Name)));
        }
    }
}
