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
            Assert.True(typeof(string).IsScalar());
            Assert.True(typeof(byte).IsScalar());
            Assert.True(typeof(short).IsScalar());
            Assert.True(typeof(int).IsScalar());
            Assert.True(typeof(long).IsScalar());
            Assert.True(typeof(char).IsScalar());
            Assert.True(typeof(float).IsScalar());
            Assert.True(typeof(double).IsScalar());
            Assert.True(typeof(bool).IsScalar());
            Assert.True(typeof(decimal).IsScalar());
            Assert.True(typeof(sbyte).IsScalar());
            Assert.True(typeof(ushort).IsScalar());
            Assert.True(typeof(uint).IsScalar());
            Assert.True(typeof(ulong).IsScalar());
            Assert.True(typeof(byte?).IsScalar());
            Assert.True(typeof(short?).IsScalar());
            Assert.True(typeof(int?).IsScalar());
            Assert.True(typeof(long?).IsScalar());
            Assert.True(typeof(char?).IsScalar());
            Assert.True(typeof(float?).IsScalar());
            Assert.True(typeof(double?).IsScalar());
            Assert.True(typeof(bool?).IsScalar());
            Assert.True(typeof(decimal?).IsScalar());
            Assert.True(typeof(sbyte?).IsScalar());
            Assert.True(typeof(ushort?).IsScalar());
            Assert.True(typeof(uint?).IsScalar());
            Assert.True(typeof(ulong?).IsScalar());
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
            Assert.Equal(typeof(string), parameters[0].Key);
            Assert.Equal(typeof(int), parameters[1].Key);
            Assert.Equal(typeof(int?), parameters[2].Key);
            Assert.Equal(typeof(List<string>), parameters[3].Key);
            Assert.Equal("string", parameters[0].Value);
            Assert.Equal("int", parameters[1].Value);
            Assert.Equal("nullableInt", parameters[2].Value);
            Assert.Equal("list", parameters[3].Value);
        }

        [Fact]
        public void FixUpTypeName_NestedGenerics_IsOk()
        {
            var type = typeof(List<List<string>>);
            Assert.Equal("List<List<String>>", type.FixUpTypeName());
        }

        [Fact]
        public void FixUpTypeName_Nullable_IsOk()
        {
            var type = typeof(int?);
            Assert.Equal("int?", type.FixUpTypeName());
        }

        [Fact]
        public void FixUpTypeName_Primitives_ConvertedToEasyToReadFormat()
        {
            Assert.Equal("string", typeof(string).FixUpScalarTypeName());
            Assert.Equal("byte", typeof(byte).FixUpScalarTypeName());
            Assert.Equal("byte[]", typeof(byte[]).FixUpScalarTypeName());
            Assert.Equal("short", typeof(short).FixUpScalarTypeName());
            Assert.Equal("int", typeof(int).FixUpScalarTypeName());
            Assert.Equal("long", typeof(long).FixUpScalarTypeName());
            Assert.Equal("char", typeof(char).FixUpScalarTypeName());
            Assert.Equal("float", typeof(float).FixUpScalarTypeName());
            Assert.Equal("double", typeof(double).FixUpScalarTypeName());
            Assert.Equal("bool", typeof(bool).FixUpScalarTypeName());
            Assert.Equal("decimal", typeof(decimal).FixUpScalarTypeName());
            Assert.Equal("sbyte", typeof(sbyte).FixUpScalarTypeName());
            Assert.Equal("ushort", typeof(ushort).FixUpScalarTypeName());
            Assert.Equal("uint", typeof(uint).FixUpScalarTypeName());
            Assert.Equal("ulong", typeof(ulong).FixUpScalarTypeName());
            Assert.Equal("object", typeof(object).FixUpScalarTypeName());
        }

        [Fact]
        public void FixUpTypeName_ReturnTypeOfVoidMethod_ConvertedToEasyToReadFormat()
        {
            var method = typeof(TwitchyObject).GetMethod("MethodThatReturnsNothing");
            var returnType = method.ReturnType.FixUpTypeName();
            Assert.Equal("void", returnType);
        }

        [Fact]
        public void FixUpTypeName_ReturnTypeWithGenericArguments_ConvertsProperly()
        {
            var method = typeof(CollectionExtensions).GetMethod("ForEach");
            var returnType = method.ReturnType.FixUpTypeName();
            Assert.Equal("IEnumerable<T>", returnType);
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

        //[Fact]
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

        //    Assert.Equal(false,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

        //    Assert.Equal(true,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(query));

        //    Assert.Equal(false,
        //        typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(command));

        //    Assert.Equal(true, command.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

        //    Assert.Equal(true, query.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));
        //}
    }
}