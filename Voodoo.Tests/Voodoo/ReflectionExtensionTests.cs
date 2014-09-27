using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ReflectionExtensionTests
    {
        [TestMethod]
        public void IsScalar_Type_WorksAsExpected()
        {
            Assert.AreEqual(true, typeof(System.String).IsScalar());
            Assert.AreEqual(true, typeof(System.Byte).IsScalar());
            Assert.AreEqual(true, typeof(System.Int16).IsScalar());
            Assert.AreEqual(true, typeof(System.Int32).IsScalar());
            Assert.AreEqual(true, typeof(System.Int64).IsScalar());
            Assert.AreEqual(true, typeof(System.Char).IsScalar());
            Assert.AreEqual(true, typeof(System.Single).IsScalar());
            Assert.AreEqual(true, typeof(System.Double).IsScalar());
            Assert.AreEqual(true, typeof(System.Boolean).IsScalar());
            Assert.AreEqual(true, typeof(System.Decimal).IsScalar());
            Assert.AreEqual(true, typeof(System.SByte).IsScalar());
            Assert.AreEqual(true, typeof(System.UInt16).IsScalar());
            Assert.AreEqual(true, typeof(System.UInt32).IsScalar());
            Assert.AreEqual(true, typeof(System.UInt64).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Byte>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Int16>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Int32>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Int64>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Char>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Single>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Double>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Boolean>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.Decimal>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.SByte>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.UInt16>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.UInt32>).IsScalar());
            Assert.AreEqual(true, typeof(Nullable<System.UInt64>).IsScalar());
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
            Assert.AreEqual(4,parameters.Count);
            Assert.AreEqual(typeof(string),parameters[0].Key);
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
            var type = typeof (List<List<string>>);
            Assert.AreEqual("List<List<String>>", type.FixUpTypeName());
        }

        [TestMethod]
        public void FixUpTypeName_Nullable_IsOk()
        {
            var type = typeof (Nullable<int>);
            Assert.AreEqual("int?", type.FixUpTypeName());
        }

        [TestMethod]
        public void FixUpTypeName_Primitives_ConvertedToEasyToReadFormat()
        {
            Assert.AreEqual(typeof(System.String).FixUpScalarTypeName(), "string");
            Assert.AreEqual(typeof(System.Byte).FixUpScalarTypeName(), "byte");
            Assert.AreEqual(typeof(System.Byte[]).FixUpScalarTypeName(), "byte[]");
            Assert.AreEqual(typeof(System.Int16).FixUpScalarTypeName(), "short");
            Assert.AreEqual(typeof(System.Int32).FixUpScalarTypeName(), "int");
            Assert.AreEqual(typeof(System.Int64).FixUpScalarTypeName(), "long");
            Assert.AreEqual(typeof(System.Char).FixUpScalarTypeName(), "char");
            Assert.AreEqual(typeof(System.Single).FixUpScalarTypeName(), "float");
            Assert.AreEqual(typeof(System.Double).FixUpScalarTypeName(), "double");
            Assert.AreEqual(typeof(System.Boolean).FixUpScalarTypeName(), "bool");
            Assert.AreEqual(typeof(System.Decimal).FixUpScalarTypeName(), "decimal");
            Assert.AreEqual(typeof(System.SByte).FixUpScalarTypeName(), "sbyte");
            Assert.AreEqual(typeof(System.UInt16).FixUpScalarTypeName(), "ushort");
            Assert.AreEqual(typeof(System.UInt32).FixUpScalarTypeName(), "uint");
            Assert.AreEqual(typeof(System.UInt64).FixUpScalarTypeName(), "ulong");
            Assert.AreEqual(typeof(System.Object).FixUpScalarTypeName(), "object");
            


        }

        [TestMethod]
        public void FixUpTypeName_ReturnTypeOfVoidMethod_ConvertedToEasyToReadFormat()
        {
            var method = typeof (IoNic).GetMethod("ShellExecute");
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
            
            var properties = typeof (ClassToReflect).GetProperties();
            return properties;
        }

        [TestMethod]
        public void IsGenericTypeInheritedFromOtherGenericType_IsInherited_ReturnsTrue()
        {
            var query =
                typeof (Response).Assembly.GetTypes()
                    .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Query"));

            var command =
                typeof (Response).Assembly.GetTypes()
                    .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Command"));

            var executor =
                typeof (Response).Assembly.GetTypes()
                    .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Executor"));

            Assert.AreEqual(false,
                typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

            Assert.AreEqual(true,
                typeof(ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(query));

            Assert.AreEqual(false,
                typeof(ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(command));

            Assert.AreEqual(true, command.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

            Assert.AreEqual(true, query.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));
                

        }
    }
}
