using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Tests.TestClasses;


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
            test = "yes".To<bool>();
            Assert.AreEqual(true, test);
            test = "true".To<bool>();
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
            Int64? test = null;
            var converted = test.To<Int64>();
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
            Decimal? test = null;
            var converted = test.To<Decimal>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullDouble_Returns0()
        {
            Double? test = null;
            var converted = test.To<Double>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullInt16_Returns0()
        {
            Int16? test = null;
            var converted = test.To<Int16>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullInt64_Returns0()
        {
            Int64? test = null;
            var converted = test.To<Int64>();
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
            Single? test = null;
            var converted = test.To<Single>();
            Assert.AreEqual(0, converted);
        }


        [TestMethod]
        public void To_NullUInt16_Returns0()
        {
            UInt16? test = null;
            var converted = test.To<UInt16>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullUInt32_Returns0()
        {
            UInt32? test = null;
            var converted = test.To<UInt32>();
            Assert.AreEqual((UInt32) 0, converted);
        }

        [TestMethod]
        public void To_NullUInt64_Returns0()
        {
            UInt64? test = null;
            var converted = test.To<UInt64>();
            Assert.AreEqual((UInt64) 0, converted);
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
            var converted = test.To<Int64>();
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
            var converted = test.To<Decimal>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectDouble_Returns0()
        {
            object test = null;
            var converted = test.To<Double>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectInt16_Returns0()
        {
            object test = null;
            var converted = test.To<Int16>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectInt64_Returns0()
        {
            object test = null;
            var converted = test.To<Int64>();
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
            var converted = test.To<Single>();
            Assert.AreEqual(0, converted);
        }


        [TestMethod]
        public void To_NullObjectUInt16_Returns0()
        {
            object test = null;
            var converted = test.To<UInt16>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_NullObjectUInt32_Returns0()
        {
            object test = null;
            var converted = test.To<UInt32>();
            Assert.AreEqual((UInt32) 0, converted);
        }

        [TestMethod]
        public void To_NullObjectUInt64_Returns0()
        {
            object test = null;
            var converted = test.To<UInt64>();
            Assert.AreEqual((UInt64) 0, converted);
        }

        [TestMethod]
        public void To_EmptyStringLong_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<Int64>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringbyte_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<byte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringchar_ReturnsNonNullChar()
        {
            var test = String.Empty;
            var converted = test.To<char>();
            Assert.IsNotNull(converted);
        }

        [TestMethod]
        public void To_EmptyStringDecimal_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<Decimal>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringDouble_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<Double>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringInt16_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<Int16>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringInt64_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<Int64>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringsbyte_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<sbyte>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringSingle_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<Single>();
            Assert.AreEqual(0, converted);
        }


        [TestMethod]
        public void To_EmptyStringUInt16_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<UInt16>();
            Assert.AreEqual(0, converted);
        }

        [TestMethod]
        public void To_EmptyStringUInt32_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<UInt32>();
            Assert.AreEqual((UInt32) 0, converted);
        }

        [TestMethod]
        public void To_EmptyStringUInt64_Returns0()
        {
            var test = String.Empty;
            var converted = test.To<UInt64>();
            Assert.AreEqual((UInt64) 0, converted);
        }

        [TestMethod]
        public void To_AnyNumeric_Converts()
        {
            Assert.AreEqual((Byte) 7, 7.To<Byte>());
            Assert.AreEqual(7, 7.To<Decimal>());
            Assert.AreEqual(7, 7.To<Double>());
            Assert.AreEqual((Int16) 7, 7.To<Int16>());
            Assert.AreEqual(7, 7.To<Int32>());
            Assert.AreEqual(7, 7.To<Int64>());
            Assert.AreEqual((SByte) 7, 7.To<SByte>());
            Assert.AreEqual(7, 7.To<Single>());
            Assert.AreEqual((UInt16) 7, 7.To<UInt16>());
            Assert.AreEqual((UInt32) 7, 7.To<UInt32>());
            Assert.AreEqual((UInt64) 7, 7.To<UInt64>());
        }


        [TestMethod]
        public void As_NullDouble_ReturnsNull()
        {
            Double? test = null;
            var converted = test.As<Double?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullInt16_ReturnsNull()
        {
            Int16? test = null;
            var converted = test.As<Int16?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullInt64_ReturnsNull()
        {
            Int64? test = null;
            var converted = test.As<Int64?>();
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
            Single? test = null;
            var converted = test.As<Single?>();
            Assert.AreEqual(null, converted);
        }


        [TestMethod]
        public void As_NullUInt16_ReturnsNull()
        {
            UInt16? test = null;
            var converted = test.As<UInt16?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullUInt32_ReturnsNull()
        {
            UInt32? test = null;
            var converted = test.As<UInt32?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullUInt64_ReturnsNull()
        {
            UInt64? test = null;
            var converted = test.As<UInt64?>();
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
            var converted = test.As<Int64?>();
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
            var converted = test.As<Decimal?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectDouble_ReturnsNull()
        {
            object test = null;
            var converted = test.As<Double?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectInt16_ReturnsNull()
        {
            object test = null;
            var converted = test.As<Int16?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectInt64_ReturnsNull()
        {
            object test = null;
            var converted = test.As<Int64?>();
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
            var converted = test.As<Single?>();
            Assert.AreEqual(null, converted);
        }


        [TestMethod]
        public void As_NullObjectUInt16_ReturnsNull()
        {
            object test = null;
            var converted = test.As<UInt16?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectUInt32_ReturnsNull()
        {
            object test = null;
            var converted = test.As<UInt32?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_NullObjectUInt64_ReturnsNull()
        {
            object test = null;
            var converted = test.As<UInt64?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringLong_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<Int64?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringbyte_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<byte?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringchar_ReturnsNonNullChar()
        {
            var test = String.Empty;
            var converted = test.As<char>();
            Assert.IsNotNull(converted);
        }

        [TestMethod]
        public void As_EmptyStringDecimal_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<Decimal?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringDouble_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<Double?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringInt16_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<Int16?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringInt64_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<Int64?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringsbyte_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<sbyte?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringSingle_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<Single?>();
            Assert.AreEqual(null, converted);
        }


        [TestMethod]
        public void As_EmptyStringUInt16_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<UInt16?>();
            Assert.AreEqual(null, converted);
        }

        [TestMethod]
        public void As_EmptyStringUInt32_ReturnsNull()
        {
            var test = String.Empty;
            var converted = test.As<UInt32?>();
            Assert.AreEqual(null, converted);
        }
    }
}