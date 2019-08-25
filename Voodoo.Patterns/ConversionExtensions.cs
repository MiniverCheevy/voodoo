using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Voodoo.Operations;

namespace Voodoo
{
    public static class ConversionExtensions
    {
        private static readonly CustomMapping[] customMappings =
        {
            new CustomMapping
            {
                Type = typeof(bool),
                ReturnValue = true,
                Values = new object[] {1, "1", "y", "yes", "Y", "Yes", "true"}
            }
        };

        private static readonly Dictionary<Type, object> nonNullDefaults = new Dictionary<Type, object>
        {
            {typeof(string), string.Empty},
            {typeof(bool), false},
            {typeof(DateTime), DateTime.MaxValue},
            {typeof(int), 0}
        };

        public static T As<T>(this object value)
        {
            if (value == null)
                return default(T);

            try
            {
                T returnValue;
                var result = convertValue(value, out returnValue);
                return result ? returnValue : default(T);
            }
            catch
            {
                return default(T);
            }
        }


        public static bool Is<T>(this object value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                if (value.GetType() == typeof(T))
                    return true;
                try
                {
                    if ((value == null) || converter.CanConvertFrom(null, value.GetType()))
                    {
                        converter.ConvertFrom(value);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static T To<T>(this object value)
        {
            if (value == null)
                return returnDefaultValue<T>();

            if (value is T)
                return (T)value;

            T returnValue;
            convertValue(value, out returnValue);
            return returnValue;
        }

        private static T returnDefaultValue<T>()
        {
            var type = typeof(T);
            if (nonNullDefaults.ContainsKey(type))
                return (T)nonNullDefaults[type];
            return default(T);
        }

        private static bool convertValue<T>(object value, out T converted)
        {
            var type = typeof(T);
            try
            {
                if (type.ToString().ToLower().Contains("int")  && value.ToString().Contains("."))
                {
                    value = value.ToString().Substring(0, value.ToString().IndexOf("."));
                }
            }
            catch
            {
            }

            TypeCode? typeCode = null;
            try
            {
                var defaultValue = default(T);
                if (defaultValue != null)
                    typeCode = ((IConvertible)default(T)).GetTypeCode();

                if (typeCode.HasValue && convertObject(value, typeCode.Value, out converted))
                    return true;
            }
            catch
            {
            }
            try
            {
                if (parseEnumValue(value, type, out converted))
                    return true;
            }
            catch
            {
            }
            try
            {
                value = getCustomMappedValue<T>(value);
                value = getCustomMappedValue<T>(value);
                var converter = TypeDescriptor.GetConverter(typeof(T));
                converted = (T)converter.ConvertFromInvariantString(value.ToString());
                return true;
            }
            catch
            {
            }

            try
            {
                if (value != null)
                {
                    converted = (T)value;
                    return true;
                }
            }
            catch
            {
            }

            try
            {
                converted = (T)value;
                return true;
            }
            catch
            {
            }

            converted = returnDefaultValue<T>();
            return false;
        }

        private static bool parseEnumValue<T>(object value, Type type, out T converted)
        {
            if (type.GetTypeInfo().BaseType == typeof(Enum))
            {
                object obj = null;

                var names = Enum.GetNames(type);
                var name = names.FirstOrDefault(c => c.ToLower() == value.ToString().ToLower());
                if (name != null)
                {
                    var match = Enum.Parse(type, name);
                    if (match != null)
                    {
                        converted = (T)match;
                        return true;
                    }
                }

                var values = Enum.GetValues(type);
                var val = names.FirstOrDefault(c => c.ToString().ToLower() == value.ToString().ToLower());
                if (val != null)
                {
                    var match = Enum.Parse(type, val);
                    if (match != null)
                    {
                        converted = (T)match;
                        return true;
                    }
                }

                var pairs = type.ToINameValuePairList();
                var friendlyValue = pairs.FirstOrDefault(c => c.Name.ToLower() == value.ToString().ToLower());
                obj = Enum.Parse(type, friendlyValue != null
                    ? friendlyValue.Value
                    : value.ToString());
                converted = (T)obj;
                return true;
            }
            converted = default(T);
            return false;
        }

        private static bool convertObject<T>(object value, TypeCode typeCode, out T valueToConvert)
        {
            valueToConvert = default(T);
            if (value == null)
            {
                return false;
            }

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    {
                        if (bool.TryParse(value.ToString(), out bool parsedBool))
                        {
                            valueToConvert = (T)(object)parsedBool;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Byte:
                    {
                        if (Byte.TryParse(value.ToString(), out Byte parsedByte))
                        {
                            valueToConvert = (T)(object)parsedByte;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Char:
                    {
                        if (Char.TryParse(value.ToString(), out Char parsedChar))
                        {
                            valueToConvert = (T)(object)parsedChar;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.DateTime:
                    {
                        if (DateTime.TryParse(value.ToString(), out DateTime parsedDateTime))
                        {
                            valueToConvert = (T)(object)parsedDateTime;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Decimal:
                    {
                        if (Decimal.TryParse(value.ToString(), out Decimal parsedDecimal))
                        {
                            valueToConvert = (T)(object)parsedDecimal;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Double:
                    {
                        if (Double.TryParse(value.ToString(), out Double parsedDouble))
                        {
                            valueToConvert = (T)(object)parsedDouble;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Int16:
                    {
                        if (Int16.TryParse(value.ToString(), out Int16 parsedInt16))
                        {
                            valueToConvert = (T)(object)parsedInt16;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Int32:
                    {
                        if (Int32.TryParse(value.ToString(), out Int32 parsedInt32))
                        {
                            valueToConvert = (T)(object)parsedInt32;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Int64:
                    {
                        if (Int64.TryParse(value.ToString(), out Int64 parsedInt64))
                        {
                            valueToConvert = (T)(object)parsedInt64;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.SByte:
                    {
                        if (SByte.TryParse(value.ToString(), out SByte parsedSByte))
                        {
                            valueToConvert = (T)(object)parsedSByte;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.Single:
                    {
                        if (Single.TryParse(value.ToString(), out Single parsedSingle))
                        {
                            valueToConvert = (T)(object)parsedSingle;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.String:
                    {

                        valueToConvert = (T)(object)Convert.ToString(value);
                        return true;
                    }
                case TypeCode.UInt16:
                    {
                        if (UInt16.TryParse(value.ToString(), out UInt16 parsedUInt16))
                        {
                            valueToConvert = (T)(object)parsedUInt16;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.UInt32:
                    {
                        if (UInt32.TryParse(value.ToString(), out UInt32 parsedUInt32))
                        {
                            valueToConvert = (T)(object)parsedUInt32;
                            return true;
                        }
                        return false;
                    }
                case TypeCode.UInt64:
                    {
                        if (UInt64.TryParse(value.ToString(), out UInt64 parsedUInt64))
                        {
                            valueToConvert = (T)(object)parsedUInt64;
                            return true;
                        }
                        return false;
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

        public static string ToCode(this object o, string name = "request")
        {
            var request = new ObjectEmissionRequest { Name = name, Source = o };
            var response = new ObjectEmissionQuery(request).Execute();
            if (response.IsOk)
                return response.Text;
            return response.Message;
        }

        private static string getEnumFriendlyName(object source)
        {
            if (source == null)
                return string.Empty;

            var type = source.GetType();
            var memberInfos = type.GetMember(source.ToString());
            if (memberInfos.Any())
            {
                var description = memberInfos.First().GetCustomAttributes(typeof(DescriptionAttribute),
                    false).FirstOrDefault();
                if (description != null)
                    return ((DescriptionAttribute)description).Description;

                var display = memberInfos.First().GetCustomAttributes(typeof(DisplayAttribute),
                    false).FirstOrDefault();
                if (display != null)
                    return ((DisplayAttribute)display).Name;
            }

            return null;
        }

        public static string ToFriendlyString(this object o)
        {
            if (o == null)
                return string.Empty;

            if (o is Enum)
            {
                var enumFriendlyName = getEnumFriendlyName(o);
                if (enumFriendlyName != null)
                    return enumFriendlyName;
                if (o.ToString() == "0")
                    return string.Empty;
            }

            var val = o.ToString();

            var stringBuilder = new StringBuilder();
            var lastWasCap = false;

            foreach (var c in val)
            {
                var s = c.ToString();
                var n = (int)c;
                if (n == 32 | n == 95)
                {
                    stringBuilder.Append(" ");
                    lastWasCap = false;
                }
                else if (n >= 65 & n <= 90)
                {
                    //Capital letters
                    if (!lastWasCap)
                    {
                        stringBuilder.Append(" ");
                        lastWasCap = true;
                    }
                    stringBuilder.Append(s);
                }
                else
                {
                    lastWasCap = false;
                    stringBuilder.Append(s);
                }
            }
            return stringBuilder.ToString().Trim();
        }

        private static object getCustomMappedValue<T>(object value)
        {
            if (value == null)
                return value;
            var val = value.ToString().ToLower();
            var customMapping = customMappings.FirstOrDefault(c => c.Type == typeof(T) && c.Values.Contains(val));

            if (customMapping != null)
                value = customMapping.ReturnValue;

            return value;
        }

        private class CustomMapping
        {
            public object ReturnValue { get; set; }
            public Type Type { get; set; }
            public object[] Values { get; set; }
        }
    }
}