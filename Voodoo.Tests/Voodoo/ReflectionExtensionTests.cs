using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ReflectionExtensionTests
    {
        [TestMethod]
        public void IsScalar_Object_ReturnsFalse()
        {
            var complexObject = GetProperties().First(c => c.Name == "ComplexObject");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsScalar_Int_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "Int");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsScalar_NullableInt_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "NullableInt");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsScalar_DateTime_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "DateAndTime");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsScalar_NullableDateTime_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "NullableDateAndTime");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsScalar_String_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "String");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsScalar_Enum_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "TestEnum");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetParameters_MethodHasParameters_ReturnesParametersAsString()
        {

        }
        public MethodInfo GetMethod()
        {

            var method = typeof(ClassToReflect).GetMethods().First();
            return method;
        }

        public PropertyInfo[] GetProperties()
        {
            
            var properties = typeof (ClassToReflect).GetProperties();
            return properties;
        }
    }
}
