using System;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ObjectifierTests
    {
        [TestMethod]
        public void ShallowCopy_Object_ObjectsAndScalarsAreTheSame()
        {
            var source = GetComplexClass();
            var target = Objectifyer.ShallowCopy(source);
            Assert.AreEqual(source.ComplexObject, target.ComplexObject);
            comparePrimitives(source, target);
        }

        [TestMethod]
        public void DeepCopy_Object_ObjectsAreDifferentScalarsAreTheSame()
        {
            var source = GetComplexClass();
            var target = Objectifyer.DeepCopy(source);
            Assert.AreNotEqual(source.ComplexObject, target.ComplexObject);
            comparePrimitives(source, target);
        }

        [TestMethod]
        public void ToXml_Object_PrimitivesMatchOnDeserializedObject()
        {
            var source = GetComplexClass();
            var xml = Objectifyer.ToXml(source, new Type[] {typeof(ClassWithDate)}, false);
            var target = Objectifyer.FromXml<ClassToReflect>(xml, new Type[] {typeof(ClassWithDate)});
        }

        [TestMethod]
        public void Base64Encode_Object_DecodedPrimitivesMatchOnDeserializedObject()
        {
            var source = GetComplexClass();
            var xml = Objectifyer.ToXml(source, new Type[] {typeof(ClassWithDate)}, true);
            var encoded = Objectifyer.Base64Encode(xml);
            var decoded = Objectifyer.Base64Decode(encoded);
            var target = Objectifyer.FromXml<ClassToReflect>(decoded, new Type[] {typeof(ClassWithDate)});
            comparePrimitives(source, target);
        }

        [TestMethod]
        public void ToXml_SimpleObject_PrimitivesMatchOnDeserializedObject()
        {
            var source = GetSimpleClass();
            var xml = Objectifyer.ToXml(source);
            var target = Objectifyer.FromXml<ClassWithDate>(xml);
            comparePrimitives(source, target);
        }

        [TestMethod]
        public void ToXml_SimpleObjectNoNamespaces_PrimitivesMatchOnDeserializedObject()
        {
            var source = GetSimpleClass();
            var xml = Objectifyer.ToXml(source, true);
            var target = Objectifyer.FromXml<ClassWithDate>(xml);
            comparePrimitives(source, target);
        }

        [TestMethod]
        public void ToDataContractXml_SimpleObject_PrimitivesMatchOnDeserializedObject()
        {
            var source = GetSimpleClass();
            var xml = Objectifyer.ToDataContractXml(source);
        }


        private static void comparePrimitives(ClassWithDate source, ClassWithDate target)
        {
            Assert.AreEqual(source.DateAndTime, target.DateAndTime);
        }

        private static void comparePrimitives(ClassToReflect source, ClassToReflect target)
        {
            Assert.AreEqual(source.DateAndTime, target.DateAndTime);
            Assert.AreEqual(source.Int, target.Int);
            Assert.AreEqual(source.NullableDateAndTime, target.NullableDateAndTime);
            Assert.AreEqual(source.NullableInt, target.NullableInt);
            Assert.AreEqual(source.String, target.String);
            Assert.AreEqual(source.TestEnum, target.TestEnum);
        }

        public ClassWithDate GetSimpleClass()
        {
            var response = new ClassWithDate
            {
                DateAndTime = DateTime.Now.AddDays(2)
            };
            return response;
        }

        public ClassToReflect GetComplexClass()
        {
            var response = new ClassToReflect
            {
                ComplexObject = new ClassWithDate {DateAndTime = DateTime.Now},
                DateAndTime = DateTime.Now.AddDays(1),
                Int = 32,
                NullableDateAndTime = null,
                NullableInt = null,
                String = "foo",
                TestEnum = TestEnum.Red
            };
            return response;
        }
    }
}