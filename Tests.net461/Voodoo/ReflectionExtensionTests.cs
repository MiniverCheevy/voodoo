using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ReflectionExtensionTests
    {
        [TestMethod]
        public void IsScalar_Type_WorksAsExpected()
        {
            Assert.AreEqual(true, typeof(string).IsScalar());
            Assert.AreEqual(true, typeof(byte).IsScalar());
            Assert.AreEqual(true, typeof(short).IsScalar());
            Assert.AreEqual(true, typeof(int).IsScalar());
            Assert.AreEqual(true, typeof(long).IsScalar());
            Assert.AreEqual(true, typeof(char).IsScalar());
            Assert.AreEqual(true, typeof(float).IsScalar());
            Assert.AreEqual(true, typeof(double).IsScalar());
            Assert.AreEqual(true, typeof(bool).IsScalar());
            Assert.AreEqual(true, typeof(decimal).IsScalar());
            Assert.AreEqual(true, typeof(sbyte).IsScalar());
            Assert.AreEqual(true, typeof(ushort).IsScalar());
            Assert.AreEqual(true, typeof(uint).IsScalar());
            Assert.AreEqual(true, typeof(ulong).IsScalar());
            Assert.AreEqual(true, typeof(byte?).IsScalar());
            Assert.AreEqual(true, typeof(short?).IsScalar());
            Assert.AreEqual(true, typeof(int?).IsScalar());
            Assert.AreEqual(true, typeof(long?).IsScalar());
            Assert.AreEqual(true, typeof(char?).IsScalar());
            Assert.AreEqual(true, typeof(float?).IsScalar());
            Assert.AreEqual(true, typeof(double?).IsScalar());
            Assert.AreEqual(true, typeof(bool?).IsScalar());
            Assert.AreEqual(true, typeof(decimal?).IsScalar());
            Assert.AreEqual(true, typeof(sbyte?).IsScalar());
            Assert.AreEqual(true, typeof(ushort?).IsScalar());
            Assert.AreEqual(true, typeof(uint?).IsScalar());
            Assert.AreEqual(true, typeof(ulong?).IsScalar());
        }

        [TestMethod]
        public void IsScalar_Object_ReturnsFalse()
        {
            var complexObject = GetProperties().First(c => c.Name == "ComplexObject");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsScalar_Decimal_ReturnsFalse()
        {
            var complexObject = GetProperties().First(c => c.Name == "Decimal");
            var result = complexObject.PropertyType.IsScalar();
            Assert.IsTrue(result);
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
        public void GetParameters_MethodHasParameters_ReturnsParametersAsString()
        {
            var parametersAsString = GetMethod().GetParametersForCodeGeneration();
            Assert.AreEqual("string string, int int, int? nullableInt, List<String> list", parametersAsString);
        }

        [TestMethod]
        public void GetParameterDictionary_MethodHasParameters_ReturnsParameters()
        {
            var parameters = GetMethod().GetParameterDictionary();
            Assert.AreEqual(4, parameters.Count);
            Assert.AreEqual(typeof(string), parameters[0].Key);
            Assert.AreEqual(typeof(int), parameters[1].Key);
            Assert.AreEqual(typeof(int?), parameters[2].Key);
            Assert.AreEqual(typeof(List<string>), parameters[3].Key);
            Assert.AreEqual("string", parameters[0].Value);
            Assert.AreEqual("int", parameters[1].Value);
            Assert.AreEqual("nullableInt", parameters[2].Value);
            Assert.AreEqual("list", parameters[3].Value);
        }

        [TestMethod]
        public void FixUpTypeName_NestedGenerics_IsOk()
        {
            var type = typeof(List<List<string>>);
            Assert.AreEqual("List<List<String>>", type.FixUpTypeName());
        }

        [TestMethod]
        public void FixUpTypeName_Nullable_IsOk()
        {
            var type = typeof(int?);
            Assert.AreEqual("int?", type.FixUpTypeName());
        }

        [TestMethod]
        public void FixUpTypeName_Primitives_ConvertedToEasyToReadFormat()
        {
            Assert.AreEqual(typeof(string).FixUpScalarTypeName(), "string");
            Assert.AreEqual(typeof(byte).FixUpScalarTypeName(), "byte");
            Assert.AreEqual(typeof(byte[]).FixUpScalarTypeName(), "byte[]");
            Assert.AreEqual(typeof(short).FixUpScalarTypeName(), "short");
            Assert.AreEqual(typeof(int).FixUpScalarTypeName(), "int");
            Assert.AreEqual(typeof(long).FixUpScalarTypeName(), "long");
            Assert.AreEqual(typeof(char).FixUpScalarTypeName(), "char");
            Assert.AreEqual(typeof(float).FixUpScalarTypeName(), "float");
            Assert.AreEqual(typeof(double).FixUpScalarTypeName(), "double");
            Assert.AreEqual(typeof(bool).FixUpScalarTypeName(), "bool");
            Assert.AreEqual(typeof(decimal).FixUpScalarTypeName(), "decimal");
            Assert.AreEqual(typeof(sbyte).FixUpScalarTypeName(), "sbyte");
            Assert.AreEqual(typeof(ushort).FixUpScalarTypeName(), "ushort");
            Assert.AreEqual(typeof(uint).FixUpScalarTypeName(), "uint");
            Assert.AreEqual(typeof(ulong).FixUpScalarTypeName(), "ulong");
            Assert.AreEqual(typeof(object).FixUpScalarTypeName(), "object");
        }

        [TestMethod]
        public void FixUpTypeName_ReturnTypeOfVoidMethod_ConvertedToEasyToReadFormat()
        {
            var method = typeof(TwitchyObject).GetMethod("MethodThatReturnsNothing");
            var returnType = method.ReturnType.FixUpTypeName();
            Assert.AreEqual("void", returnType);
        }

        [TestMethod]
        public void FixUpTypeName_ReturnTypeWithGenericArguments_ConvertsProperly()
        {
            var method = typeof(CollectionExtensions).GetMethod("ForEach");
            var returnType = method.ReturnType.FixUpTypeName();
            Assert.AreEqual("IEnumerable<T>", returnType);
        }

        public MethodInfo GetMethod()
        {
            var method = typeof(ClassToReflect).GetMethod("Method");
            return method;
        }

        public PropertyInfo[] GetProperties()
        {
            var properties = typeof(ClassToReflect).GetProperties();
            return properties;
        }

        //[TestMethod]
        //public void IsGenericTypeInheritedFromOtherGenericType_IsInherited_ReturnsTrue()
        //{
        //    var query =
        //        typeof (Response).GetTypeInfo().Assembly.GetTypes()
        //            .First(c => c.Namespace == "Voodoo.Operations" && c.UserName.StartsWith("Query"));

        //    var command =
        //        typeof (Response).GetTypeInfo().Assembly.GetTypes()
        //            .First(c => c.Namespace == "Voodoo.Operations" && c.UserName.StartsWith("Command"));

        //    var executor =
        //        typeof (Response).GetTypeInfo().Assembly.GetTypes()
        //            .First(c => c.Namespace == "Voodoo.Operations" && c.UserName.StartsWith("Executor"));

        //    Assert.AreEqual(false,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

        //    Assert.AreEqual(true,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(query));

        //    Assert.AreEqual(false,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(command));

        //    Assert.AreEqual(true, command.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

        //    Assert.AreEqual(true, query.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));
        //}
    }
}