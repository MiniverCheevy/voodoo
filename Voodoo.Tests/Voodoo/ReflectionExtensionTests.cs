using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo
{
    
    public class ReflectionExtensionTests
    {
        [Fact]
        public void IsScalar_Type_WorksAsExpected()
        {
            Assert.Equal(true, typeof(System.String).IsScalar());
            Assert.Equal(true, typeof(System.Byte).IsScalar());
            Assert.Equal(true, typeof(System.Int16).IsScalar());
            Assert.Equal(true, typeof(System.Int32).IsScalar());
            Assert.Equal(true, typeof(System.Int64).IsScalar());
            Assert.Equal(true, typeof(System.Char).IsScalar());
            Assert.Equal(true, typeof(System.Single).IsScalar());
            Assert.Equal(true, typeof(System.Double).IsScalar());
            Assert.Equal(true, typeof(System.Boolean).IsScalar());
            Assert.Equal(true, typeof(System.Decimal).IsScalar());
            Assert.Equal(true, typeof(System.SByte).IsScalar());
            Assert.Equal(true, typeof(System.UInt16).IsScalar());
            Assert.Equal(true, typeof(System.UInt32).IsScalar());
            Assert.Equal(true, typeof(System.UInt64).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Byte>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Int16>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Int32>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Int64>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Char>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Single>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Double>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Boolean>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.Decimal>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.SByte>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.UInt16>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.UInt32>).IsScalar());
            Assert.Equal(true, typeof(Nullable<System.UInt64>).IsScalar());
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
            Assert.Equal(4,parameters.Count);
            Assert.Equal(typeof(string),parameters[0].Key);
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
            var type = typeof (List<List<string>>);
            Assert.Equal("List<List<String>>", type.FixUpTypeName());
        }

        [Fact]
        public void FixUpTypeName_Nullable_IsOk()
        {
            var type = typeof (Nullable<int>);
            Assert.Equal("int?", type.FixUpTypeName());
        }

        [Fact]
        public void FixUpTypeName_Primitives_ConvertedToEasyToReadFormat()
        {
            Assert.Equal(typeof(System.String).FixUpScalarTypeName(), "string");
            Assert.Equal(typeof(System.Byte).FixUpScalarTypeName(), "byte");
            Assert.Equal(typeof(System.Byte[]).FixUpScalarTypeName(), "byte[]");
            Assert.Equal(typeof(System.Int16).FixUpScalarTypeName(), "short");
            Assert.Equal(typeof(System.Int32).FixUpScalarTypeName(), "int");
            Assert.Equal(typeof(System.Int64).FixUpScalarTypeName(), "long");
            Assert.Equal(typeof(System.Char).FixUpScalarTypeName(), "char");
            Assert.Equal(typeof(System.Single).FixUpScalarTypeName(), "float");
            Assert.Equal(typeof(System.Double).FixUpScalarTypeName(), "double");
            Assert.Equal(typeof(System.Boolean).FixUpScalarTypeName(), "bool");
            Assert.Equal(typeof(System.Decimal).FixUpScalarTypeName(), "decimal");
            Assert.Equal(typeof(System.SByte).FixUpScalarTypeName(), "sbyte");
            Assert.Equal(typeof(System.UInt16).FixUpScalarTypeName(), "ushort");
            Assert.Equal(typeof(System.UInt32).FixUpScalarTypeName(), "uint");
            Assert.Equal(typeof(System.UInt64).FixUpScalarTypeName(), "ulong");
            Assert.Equal(typeof(System.Object).FixUpScalarTypeName(), "object");
            


        }

        [Fact]
        public void FixUpTypeName_ReturnTypeOfVoidMethod_ConvertedToEasyToReadFormat()
        {
            var method = typeof (IoNic).GetMethod("ShellExecute");
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
            
            var properties = typeof (ClassToReflect).GetProperties();
            return properties;
        }

        [Fact]
        public void IsGenericTypeInheritedFromOtherGenericType_IsInherited_ReturnsTrue()
        {
            var query =
                typeof (Response).GetTypeInfo().Assembly.GetTypes()
                    .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Query"));

            var command =
                typeof (Response).GetTypeInfo().Assembly.GetTypes()
                    .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Command"));

            var executor =
                typeof (Response).GetTypeInfo().Assembly.GetTypes()
                    .First(c => c.Namespace == "Voodoo.Operations" && c.Name.StartsWith("Executor"));

            Assert.Equal(false,
                typeof (ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

            Assert.Equal(true,
                typeof(ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(query));

            Assert.Equal(false,
                typeof(ObjectStringificationQuery).IsGenericTypeDirectlyInheritedFromOtherGenericType(command));

            Assert.Equal(true, command.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));

            Assert.Equal(true, query.IsGenericTypeDirectlyInheritedFromOtherGenericType(executor));
                

        }
    }
}
