using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Voodoo.Operations;

namespace Voodoo
{
    public static class ConversionExtensions
    {
        private static readonly customMapping[] customMappings =
        {
            new customMapping
            {
                Type = typeof (bool),
                ReturnValue = true,
                Values = new object[] {1, "1", "y", "yes", "Y", "Yes", "true"}
            }
        };

        private static readonly Dictionary<Type, object> nonNullDefaults = new Dictionary<Type, object>
        {
            {typeof (string), string.Empty},
            {typeof (bool), false},
            {typeof (DateTime), DateTime.MaxValue},
            {typeof (int), 0}
        };

        //[DebuggerNonUserCode]
        public static T To<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return returnDefaultValue<T>();

            if (value is T)
                return (T) (object) value;

            value = value.Trim();

            return convertValue<T>(value);
        }

        //[DebuggerNonUserCode]
        public static T As<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default(T);
            try
            {
                return value.To<T>();
            }
            catch
            {
                return default(T);
            }
        }

        //[DebuggerNonUserCode]
        public static T As<T>(this object value)
        {
            if (value == null)
                return default(T);
            try
            {
                return value.To<T>();
            }
            catch
            {
                return default(T);
            }
        }

        //[DebuggerNonUserCode]
        public static T To<T>(this object value)
        {
            if (value == null)
                return returnDefaultValue<T>();

            if (value is T)
                return (T) value;

            return convertValue<T>(value);
        }

        //[DebuggerNonUserCode]
        private static T returnDefaultValue<T>()
        {
            var type = typeof (T);
            if (nonNullDefaults.ContainsKey(type))
                return (T) nonNullDefaults[type];
            return default(T);
        }

        //[DebuggerNonUserCode]
        private static T convertValue<T>(object value)
        {
            T converted;
            var type = typeof (T);
            var typeCode = Type.GetTypeCode(type);
            try
            {
                if (convertObject(value, typeCode, out converted))
                    return converted;
            }
            catch
            {
            }
            try
            {
                if (parseEnumValue(value, type, out converted))
                    return converted;
            }
            catch
            {
            }

            try
            {
                value = getCustomMappedValue<T>(value);
                var converter = TypeDescriptor.GetConverter(typeof (T));
                return (T) converter.ConvertFromInvariantString(value.ToString());
            }
            catch
            {
                return returnDefaultValue<T>();
            }
        }

        private static bool parseEnumValue<T>(object value, Type type, out T converted)
        {
            if (type.BaseType == typeof (Enum))
            {
                var obj = Enum.Parse(type, value.ToString());
                {
                    converted = (T) obj;
                    return true;
                }
            }
            converted = default(T);
            return false;
        }

        //[DebuggerNonUserCode]
        private static bool convertObject<T>(object value, TypeCode typeCode, out T valueToConvert)
        {
            valueToConvert = default(T);
            switch (typeCode)
            {
                case TypeCode.Boolean:
                {
                    valueToConvert = (T) (object) Convert.ToBoolean(value);
                    return true;
                }
                case TypeCode.Byte:
                {
                    valueToConvert = (T) (object) Convert.ToByte(value);
                    return true;
                }
                case TypeCode.Char:
                {
                    valueToConvert = (T) (object) Convert.ToChar(value);
                    return true;
                }
                case TypeCode.DateTime:
                {
                    valueToConvert = (T) (object) DateTime.Parse(value.ToString().Trim());
                    return true;
                }
                case TypeCode.Decimal:
                {
                    valueToConvert = (T) (object) Convert.ToDecimal(value);
                    return true;
                }
                case TypeCode.Double:
                {
                    valueToConvert = (T) (object) Convert.ToDouble(value);
                    return true;
                }
                case TypeCode.Int16:
                {
                    valueToConvert = (T) (object) Convert.ToInt16(value);
                    return true;
                }
                case TypeCode.Int32:
                {
                    valueToConvert = (T) (object) Convert.ToInt32(value);
                    return true;
                }
                case TypeCode.Int64:
                {
                    valueToConvert = (T) (object) Convert.ToInt64(value);
                    return true;
                }
                case TypeCode.SByte:
                {
                    valueToConvert = (T) (object) Convert.ToSByte(value);
                    return true;
                }
                case TypeCode.Single:
                {
                    valueToConvert = (T) (object) Convert.ToSingle(value);
                    return true;
                }
                case TypeCode.String:
                {
                    valueToConvert = (T) (object) Convert.ToString(value);
                    return true;
                }
                case TypeCode.UInt16:
                {
                    valueToConvert = (T) (object) Convert.ToUInt16(value);
                    return true;
                }
                case TypeCode.UInt32:
                {
                    valueToConvert = (T) (object) Convert.ToUInt32(value);
                    return true;
                }
                case TypeCode.UInt64:
                {
                    valueToConvert = (T) (object) Convert.ToUInt64(value);
                    return true;
                }
                case TypeCode.DBNull:
                case TypeCode.Empty:
                default:
                    break;
            }
            return false;
        }

        public static DateTime ToStartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public static DateTime ToEndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static string ToDebugString(this object o)
        {
            var response = new ObjectStringificationQuery(o).Execute();
            if (response.IsOk)
                return response.Text;
            return response.Message;
        }

        [DebuggerStepThrough]
        public static string ToFriendlyString(this object o)
        {
            var val = To<string>(o);

            var ret = new StringBuilder();
            var lastWasCap = false;

            foreach (var c in val)
            {
                var s = c.ToString();
                var n = (int) c;
                if (n == 32 | n == 95)
                {
                    ret.Append(" ");
                    lastWasCap = false;
                }
                else if (n >= 65 & n <= 90)
                {
                    //Capital letters
                    if (!lastWasCap)
                    {
                        ret.Append(" ");
                        lastWasCap = true;
                    }
                    ret.Append(s);
                }
                else
                {
                    lastWasCap = false;
                    ret.Append(s);
                }
            }
            return ret.ToString().Trim();
        }

        private static object getCustomMappedValue<T>(object value)
        {
            var customMapping = customMappings.FirstOrDefault(c => c.Type == typeof (T) && c.Values.Contains(value));

            if (customMapping != null)
                value = customMapping.ReturnValue;

            return value;
        }

        private class customMapping
        {
            public Object ReturnValue { get; set; }

            public Type Type { get; set; }

            public Object[] Values { get; set; }
        }
    }
}