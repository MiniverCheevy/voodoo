using System;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class ConversionExtensionTests
    {
        [TestMethod]
        public void ToFriendlyString_String_SpacedProperly()
        {
            var test = "UserId";
            var friendly = test.ToFriendlyString();
            Assert.AreEqual("User Id", friendly);
            test = "NASA";
            friendly = test.ToFriendlyString();
            Assert.AreEqual("NASA", friendly);
        }

       
        [TestMethod]
		public void ToFriendlyString_Enum_UsesDescriptionThenDisplayThenAddsSpaces()
		{
			var test = TestEnumWithDescriptionAndDisplay.Red;
			var friendly = test.ToFriendlyString();
			Assert.AreEqual("Crimson", friendly);
			test = TestEnumWithDescriptionAndDisplay.Blue;
			friendly = test.ToFriendlyString();
			Assert.AreEqual("Azure", friendly);
			test = TestEnumWithDescriptionAndDisplay.RedOrangeYellow;
			friendly = test.ToFriendlyString();
			Assert.AreEqual("Red Orange Yellow", friendly);

		}

        [TestMethod]
        public void ToFriendlyString_EnumIsInvalid_ReturnsEmptyStringInsteadOfZero()
        {
            var test = "Yellow".To<TestEnumWithDescriptionAndDisplay>().ToFriendlyString();            
            Assert.AreEqual(string.Empty,test);

        }

        [TestMethod]
        public void FromFriendlyString_ToEnum_HandlesValuesFromDescriptionDisplayAndFriendlyfied()
        {
            var test = TestEnumWithDescriptionAndDisplay.Red;
            var friendly = test.ToFriendlyString();
            Assert.AreEqual(test, friendly.To<TestEnumWithDescriptionAndDisplay>());

            test = TestEnumWithDescriptionAndDisplay.Blue;
            friendly = test.ToFriendlyString();
            Assert.AreEqual(test, friendly.To<TestEnumWithDescriptionAndDisplay>());

            test = TestEnumWithDescriptionAndDisplay.RedOrangeYellow;
            friendly = test.ToFriendlyString();
            Assert.AreEqual(test, friendly.To<TestEnumWithDescriptionAndDisplay>());

        }

        [TestMethod]
        public void Values_ToEnum_HandlesValuesFromDescriptionDisplayAndFriendlyfied()
        {
            var test = TestEnumWithDescriptionAndDisplay.Red;            
            Assert.AreEqual(test, 1.To<TestEnumWithDescriptionAndDisplay>());

            test = TestEnumWithDescriptionAndDisplay.Blue;
            Assert.AreEqual(test, "2".To<TestEnumWithDescriptionAndDisplay>());

        }


        [TestMethod]
		public void ToFriendlyString_Null_DoesNotThrow()
		{
			var defaultTest = default(TestEnumWithDescriptionAndDisplay).ToFriendlyString();
			var nullableTest = default(TestEnumWithDescriptionAndDisplay?).ToFriendlyString();
			var stringTest = default(string).ToFriendlyString();
		}

		[TestMethod]
        public void ToStartOfDay_Date_TimeStripped()
        {
            var date = "1/1/1900 12:36:54:123".As<DateTime>();
            var strippedDate = date.ToStartOfDay();
            Assert.AreEqual(0, strippedDate.Hour);
            Assert.AreEqual(0, strippedDate.Minute);
            Assert.AreEqual(0, strippedDate.Second);
            Assert.AreEqual(0, strippedDate.Millisecond);
        }

        [TestMethod]
        public void ToEndOfDay_Date_TimesMaxed()
        {
            var date = "1/1/1900 12:36:54:123".As<DateTime>();
            var strippedDate = date.ToEndOfDay();
            Assert.AreEqual(23, strippedDate.Hour);
            Assert.AreEqual(59, strippedDate.Minute);
            Assert.AreEqual(59, strippedDate.Second);
            Assert.AreEqual(999, strippedDate.Millisecond);
        }

        [TestMethod]
        public void To_StringAndNumericData_ConvertedToEnum()
        {
            var test = "red".To<TestEnum>();
            Assert.AreEqual(TestEnum.Red, test);
            test = "Red".To<TestEnum>();
            Assert.AreEqual(TestEnum.Red, test);
            test = "1".To<TestEnum>();
            Assert.AreEqual(TestEnum.Red, test);
            test = 1.To<TestEnum>();
            Assert.AreEqual(TestEnum.Red, test);
        }

        [TestMethod]
        public void To_BoolCustomMapping_ReturnsTrue()
        {
            var test = "y".To<bool>();
            Assert.AreEqual(true, test);
            test = "yes".To<bool>();
            Assert.AreEqual(true, test);
            test = "true".To<bool>();
            Assert.AreEqual(true, test);
            test = "Y".To<bool>();
            Assert.AreEqual(true, test);
            test = "YES".To<bool>();
            Assert.AreEqual(true, test);
            test = "True".To<bool>();
            Assert.AreEqual(true, test);
            test = "1".To<bool>();
            Assert.AreEqual(true, test);
            test = 1.To<bool>();
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void To_NullString_ReturnsEmptyString()
        {
            string test = null;
            var converted = test.To<string>();
            Assert.AreEqual(string.Empty, converted);
        }

        [TestMethod]
        public void To_NullBool_ReturnsFalse()
        {
            bool? test = null;
            var converted = test.To<bool>();
            Assert.AreEqual(false, converted);
        }

        [TestMethod]
        public void To_NullLong_Returns0()
        {
            long? test = null;
            var converted = test.To<long>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_Nullbyte_Returns0()
        {
            byte? test = null;
            var converted = test.To<byte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_Nullchar_ReturnsNonNullChar()
        {
            char? test = null;
            var converted = test.To<char>();
            Assert.IsNotNull(converted);
        }

        [TestMethod]
        public void To_NullDecimal_Returns0()
        {
            decimal? test = null;
            var converted = test.To<decimal>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullDouble_Returns0()
        {
            double? test = null;
            var converted = test.To<double>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullInt16_Returns0()
        {
            short? test = null;
            var converted = test.To<short>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullInt64_Returns0()
        {
            long? test = null;
            var converted = test.To<long>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_Nullsbyte_Returns0()
        {
            sbyte? test = null;
            var converted = test.To<sbyte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullSingle_Returns0()
        {
            float? test = null;
            var converted = test.To<float>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullUInt16_Returns0()
        {
            ushort? test = null;
            var converted = test.To<ushort>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullUInt32_Returns0()
        {
            uint? test = null;
            var converted = test.To<uint>();
            Assert.AreEqual((uint) 0, converted);
        }

        [TestMethod]
        public void To_NullUInt64_Returns0()
        {
            ulong? test = null;
            var converted = test.To<ulong>();
            Assert.AreEqual((ulong) 0, converted);
        }

        [TestMethod]
        public void To_NullObjectString_ReturnsEmptyString()
        {
            object test = null;
            var converted = test.To<string>();
            Assert.AreEqual(string.Empty, converted);
        }

        [TestMethod]
        public void To_NullObjectBool_ReturnsFalse()
        {
            object test = null;
            var converted = test.To<bool>();
            Assert.AreEqual(false, converted);
        }

        [TestMethod]
        public void To_NullObjectLong_Returns0()
        {
            object test = null;
            var converted = test.To<long>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectbyte_Returns0()
        {
            object test = null;
            var converted = test.To<byte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectchar_ReturnsNonNullChar()
        {
            object test = null;
            var converted = test.To<char>();
            Assert.IsNotNull(converted);
        }

        [TestMethod]
        public void To_NullObjectDecimal_Returns0()
        {
            object test = null;
            var converted = test.To<decimal>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectDouble_Returns0()
        {
            object test = null;
            var converted = test.To<double>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectInt16_Returns0()
        {
            object test = null;
            var converted = test.To<short>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectInt64_Returns0()
        {
            object test = null;
            var converted = test.To<long>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectsbyte_Returns0()
        {
            object test = null;
            var converted = test.To<sbyte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectSingle_Returns0()
        {
            object test = null;
            var converted = test.To<float>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectUInt16_Returns0()
        {
            object test = null;
            var converted = test.To<ushort>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectUInt32_Returns0()
        {
            object test = null;
            var converted = test.To<uint>();
            Assert.AreEqual((uint) 0, converted);
        }

        [TestMethod]
        public void To_NullObjectUInt64_Returns0()
        {
            object test = null;
            var converted = test.To<ulong>();
            Assert.AreEqual((ulong) 0, converted);
        }

        [TestMethod]
        public void To_EmptyStringLong_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<long>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringbyte_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<byte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringchar_ReturnsNonNullChar()
        {
            var test = string.Empty;
            var converted = test.To<char>();
            Assert.IsNotNull(converted);
        }

        [TestMethod]
        public void To_EmptyStringDecimal_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<decimal>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringDouble_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<double>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringInt16_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<short>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringInt64_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<long>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringsbyte_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<sbyte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringSingle_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<float>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringUInt16_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<ushort>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringUInt32_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<uint>();
            Assert.AreEqual((uint) 0, converted);
        }

        [TestMethod]
        public void To_EmptyStringUInt64_Returns0()
        {
            var test = string.Empty;
            var converted = test.To<ulong>();
            Assert.AreEqual((ulong) 0, converted);
        }

        [TestMethod]
        public void To_AnyNumeric_Converts()
        {
            Assert.AreEqual((byte) 7, 7.To<byte>());
            Assert.AreEqual(7, 7.To<decimal>());
            Assert.AreEqual(7, 7.To<double>());
            Assert.AreEqual((short) 7, 7.To<short>());
            Assert.AreEqual(7, 7.To<int>());
            Assert.AreEqual(7, 7.To<long>());
            Assert.AreEqual((sbyte) 7, 7.To<sbyte>());
            Assert.AreEqual(7, 7.To<float>());
            Assert.AreEqual((ushort) 7, 7.To<ushort>());
            Assert.AreEqual((uint) 7, 7.To<uint>());
            Assert.AreEqual((ulong) 7, 7.To<ulong>());
        }

        [TestMethod]
        public void As_StringDouble_ReturnsNull()
        {
            var test = "testString";
            var converted = test.As<double?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullDouble_ReturnsNull()
        {
            double? test = null;
            var converted = test.As<double?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullInt16_ReturnsNull()
        {
            short? test = null;
            var converted = test.As<short?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullInt64_ReturnsNull()
        {
            long? test = null;
            var converted = test.As<long?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_Nullsbyte_ReturnsNull()
        {
            sbyte? test = null;
            var converted = test.As<sbyte?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullSingle_ReturnsNull()
        {
            float? test = null;
            var converted = test.As<float?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullUInt16_ReturnsNull()
        {
            ushort? test = null;
            var converted = test.As<ushort?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullUInt32_ReturnsNull()
        {
            uint? test = null;
            var converted = test.As<uint?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullUInt64_ReturnsNull()
        {
            ulong? test = null;
            var converted = test.As<ulong?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectString_ReturnsNull()
        {
            object test = null;
            var converted = test.As<string>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectBool_ReturnsFalse()
        {
            object test = null;
            var converted = test.As<bool?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectLong_ReturnsNull()
        {
            object test = null;
            var converted = test.As<long?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectbyte_ReturnsNull()
        {
            object test = null;
            var converted = test.As<byte?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectchar_ReturnsNonNullChar()
        {
            object test = null;
            var converted = test.As<char>();
            Assert.IsNotNull(converted);
        }

        [TestMethod]
        public void As_NullObjectDecimal_ReturnsNull()
        {
            object test = null;
            var converted = test.As<decimal?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectDouble_ReturnsNull()
        {
            object test = null;
            var converted = test.As<double?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectInt16_ReturnsNull()
        {
            object test = null;
            var converted = test.As<short?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectInt64_ReturnsNull()
        {
            object test = null;
            var converted = test.As<long?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectsbyte_ReturnsNull()
        {
            object test = null;
            var converted = test.As<sbyte?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectSingle_ReturnsNull()
        {
            object test = null;
            var converted = test.As<float?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectUInt16_ReturnsNull()
        {
            object test = null;
            var converted = test.As<ushort?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectUInt32_ReturnsNull()
        {
            object test = null;
            var converted = test.As<uint?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectUInt64_ReturnsNull()
        {
            object test = null;
            var converted = test.As<ulong?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringLong_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<long?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringbyte_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<byte?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringchar_ReturnsNonNullChar()
        {
            var test = string.Empty;
            var converted = test.As<char>();
            Assert.IsNotNull(converted);
        }

        [TestMethod]
        public void As_EmptyStringDecimal_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<decimal?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringDouble_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<double?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringInt16_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<short?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringInt64_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<long?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringsbyte_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<sbyte?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringSingle_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<float?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringUInt16_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<ushort?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringUInt32_ReturnsNull()
        {
            var test = string.Empty;
            var converted = test.As<uint?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void To_DecimalAsStringToInt_ReturnsInt()
        {
            var test = "5.00";
            const int expected = 5;
            var converted = test.To<int>();
            Assert.AreEqual(expected, converted);
        }

        [TestMethod]
        public void To_DecimalToInt_ReturnsInt()
        {
            var test = 5.00M;
            const int expected = 5;
            var converted = test.To<int>();
            Assert.AreEqual(expected, converted);
        }
    }
}