using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo
{
    public class ReflectionExtensionTests
    {
        [Fact]
        public void IsScalar_Type_WorksAsExpected()
        {
            Assert.Equal(true, typeof (string).IsScalar());
            Assert.Equal(true, typeof (byte).IsScalar());
            Assert.Equal(true, typeof (short).IsScalar());
            Assert.Equal(true, typeof (int).IsScalar());
            Assert.Equal(true, typeof (long).IsScalar());
            Assert.Equal(true, typeof (char).IsScalar());
            Assert.Equal(true, typeof (float).IsScalar());
            Assert.Equal(true, typeof (double).IsScalar());
            Assert.Equal(true, typeof (bool).IsScalar());
            Assert.Equal(true, typeof (decimal).IsScalar());
            Assert.Equal(true, typeof (sbyte).IsScalar());
            Assert.Equal(true, typeof (ushort).IsScalar());
            Assert.Equal(true, typeof (uint).IsScalar());
            Assert.Equal(true, typeof (ulong).IsScalar());
            Assert.Equal(true, typeof (byte?).IsScalar());
            Assert.Equal(true, typeof (short?).IsScalar());
            Assert.Equal(true, typeof (int?).IsScalar());
            Assert.Equal(true, typeof (long?).IsScalar());
            Assert.Equal(true, typeof (char?).IsScalar());
            Assert.Equal(true, typeof (float?).IsScalar());
            Assert.Equal(true, typeof (double?).IsScalar());
            Assert.Equal(true, typeof (bool?).IsScalar());
            Assert.Equal(true, typeof (decimal?).IsScalar());
            Assert.Equal(true, typeof (sbyte?).IsScalar());
            Assert.Equal(true, typeof (ushort?).IsScalar());
            Assert.Equal(true, typeof (uint?).IsScalar());
            Assert.Equal(true, typeof (ulong?).IsScalar());
        }

        [Fact]
        public void IsScalar_Object_ReturnsFalse()
        {
            var complexObject = GetProperties().First(c => c.Name == "ComplexObject");
            var result = complexObject.PropertyType.IsScalar();
            Assert.False(result);
        }

        [Fact]
        public void IsScalar_Decimal_ReturnsFalse()
        {
            var complexObject = GetProperties().First(c => c.Name == "Decimal");
            var result = complexObject.PropertyType.IsScalar();
            Assert.True(result);
        }

        [Fact]
        public void IsScalar_Int_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "Int");
            var result = complexObject.PropertyType.IsScalar();
            Assert.True(result);
        }

        [Fact]
        public void IsScalar_NullableInt_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "NullableInt");
            var result = complexObject.PropertyType.IsScalar();
            Assert.True(result);
        }

        [Fact]
        public void IsScalar_DateTime_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "DateAndTime");
            var result = complexObject.PropertyType.IsScalar();
            Assert.True(result);
        }

        [Fact]
        public void IsScalar_NullableDateTime_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "NullableDateAndTime");
            var result = complexObject.PropertyType.IsScalar();
            Assert.True(result);
        }

        [Fact]
        public void IsScalar_String_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "String");
            var result = complexObject.PropertyType.IsScalar();
            Assert.True(result);
        }

        [Fact]
        public void IsScalar_Enum_ReturnsTrue()
        {
            var complexObject = GetProperties().First(c => c.Name == "TestEnum");
            var result = complexObject.PropertyType.IsScalar();
            Assert.True(result);
        }

        [Fact]
        public void GetParameters_MethodHasParameters_ReturnsParametersAsString()
        {
            var parametersAsString = GetMethod().GetParametersForCodeGeneration();
            Assert.Equal("string string, int int, int? nullableInt, List<String> list", parametersAsString);
        }

        [Fact]
        public void GetParameterDictionary_MethodHasParameters_ReturnsParameters()
        {
            var parameters = GetMethod().GetParameterDictionary();
            Assert.Equal(4, parameters.Count);
            Assert.Equal(typeof (string), parameters[0].Key);
            Assert.Equal(typeof (int), parameters[1].Key);
            Assert.Equal(typeof (int?), parameters[2].Key);
            Assert.Equal(typeof (List<string>), parameters[3].Key);
            Assert.Equal("string", parameters[0].Value);
            Assert.Equal("int", parameters[1].Value);
            Assert.Equal("nullableInt", parameters[2].Value);
            Assert.Equal("list", parameters[3].Value);
        }

        [Fact]
        public void FixUpTypeName_NestedGenerics_IsOk()
        {
            var type = typeof (List<List<string>>);
            Assert.Equal("List<List<String>>", type.FixUpTypeName());
        }

        [Fact]
        public void FixUpTypeName_Nullable_IsOk()
        {
            var type = typeof (int?);
            Assert.Equal("int?", type.FixUpTypeName());
        }

        [Fact]
        public void FixUpTypeName_Primitives_ConvertedToEasyToReadFormat()
        {
            Assert.Equal(typeof (string).FixUpScalarTypeName(), "string");
            Assert.Equal(typeof (byte).FixUpScalarTypeName(), "byte");
            Assert.Equal(typeof (byte[]).FixUpScalarTypeName(), "byte[]");
            Assert.Equal(typeof (short).FixUpScalarTypeName(), "short");
            Assert.Equal(typeof (int).FixUpScalarTypeName(), "int");
            Assert.Equal(typeof (long).FixUpScalarTypeName(), "long");
            Assert.Equal(typeof (char).FixUpScalarTypeName(), "char");
            Assert.Equal(typeof (float).FixUpScalarTypeName(), "float");
            Assert.Equal(typeof (double).FixUpScalarTypeName(), "double");
            Assert.Equal(typeof (bool).FixUpScalarTypeName(), "bool");
            Assert.Equal(typeof (decimal).FixUpScalarTypeName(), "decimal");
            Assert.Equal(typeof (sbyte).FixUpScalarTypeName(), "sbyte");
            Assert.Equal(typeof (ushort).FixUpScalarTypeName(), "ushort");
            Assert.Equal(typeof (uint).FixUpScalarTypeName(), "uint");
            Assert.Equal(typeof (ulong).FixUpScalarTypeName(), "ulong");
            Assert.Equal(typeof (object).FixUpScalarTypeName(), "object");
        }

        [Fact]
        public void FixUpTypeName_ReturnTypeOfVoidMethod_ConvertedToEasyToReadFormat()
        {
            var method = typeof (TwitchyObject).GetMethod("MethodThatReturnsNothing");
            var returnType = method.ReturnType.FixUpTypeName();
            Assert.Equal("void", returnType);
        }

        [Fact]
        public void FixUpTypeName_ReturnTypeWithGenericArguments_ConvertsProperly()
        {
            var method = typeof (CollectionExtensions).GetMethod("ForEach");
            var returnType = method.ReturnType.FixUpTypeName();
            Assert.Equal("IEnumerable<T>", returnType);
        }

        public MethodInfo GetMethod()
        {
            var method = typeof (ClassToReflect).GetMethod("Method");
            return method;
        }

        public PropertyInfo[] GetProperties()
        {
            var properties = typeof (ClassToReflect).GetProperties();
            return properties;
        }
#if (!PCL)
        //[Fact]
        //public void IsGenericTypeInheritedFromOtherGenericType_IsInherited_ReturnsTrue()
        //{
        //    var query =
        //        typeof (Response).GetTypeInfo().Assembly.GetTypes()
        //            .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Query"));

        //    var command =
        //        typeof (Response).GetTypeInfo().Assembly.GetTypes()
        //            .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Command"));

        //    var executor =
        //        typeof (Response).GetTypeInfo().Assembly.GetTypes()
        //            .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Executor"));

        //    Assert.Equal(false,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

        //    Assert.Equal(true,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(query));

        //    Assert.Equal(false,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(command));

        //    Assert.Equal(true, command.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

        //    Assert.Equal(true, query.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));
        //}
#endif
    }
}