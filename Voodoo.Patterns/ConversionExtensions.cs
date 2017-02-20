using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Voodoo.Operations;
#if !PCL && !NETCOREAPP1_0
using System.ComponentModel.DataAnnotations;
#endif
namespace Voodoo
{
	public static class ConversionExtensions
	{
		private static readonly CustomMapping[] customMappings =
		{
			new CustomMapping
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
				if (type.ToString().ToLower().Contains("int") && value is string && value.ToString().Contains("."))
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
#if PCL
                typeCode = typeof (T).GetTypeCode();
#else
				var defaultValue = default(T);
				if (defaultValue != null)
					typeCode = ((IConvertible)default(T)).GetTypeCode();
#endif
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
#if !PCL && !NETCOREAPP1_0
			try
			{
				value = getCustomMappedValue<T>(value);
				var converter = TypeDescriptor.GetConverter(typeof(T));
				converted = (T)converter.ConvertFromInvariantString(value.ToString());
				return true;
			}
			catch
			{
			}
#else
            try
            {
                value = getCustomMappedValue<T>(value);
                if (value != null)
                {
                    converted = (T) value;
                    return true;
                }
            }
            catch
            {
            }
#endif
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

#if (!PCL)
                object obj = null;
                var friendlyValue = typeof(Enum).ToINameValuePairList().FirstOrDefault(c=>c.Name == value.ToString ());
			    obj = Enum.Parse(type, friendlyValue != null ? 
                    friendlyValue.Value : value.ToString());
			    converted = (T)obj;
				return true;
#else
                var obj = typeof (T).ParseEnum(value.ToString());
                converted = (T)obj;
                return true;
#endif
			}
			converted = default(T);
			return false;
		}

		private static bool convertObject<T>(object value, TypeCode typeCode, out T valueToConvert)
		{
			valueToConvert = default(T);
			switch (typeCode)
			{
				case TypeCode.Boolean:
					{
						valueToConvert = (T)(object)Convert.ToBoolean(value);
						return true;
					}
				case TypeCode.Byte:
					{
						valueToConvert = (T)(object)Convert.ToByte(value);
						return true;
					}
				case TypeCode.Char:
					{
						valueToConvert = (T)(object)Convert.ToChar(value);
						return true;
					}
				case TypeCode.DateTime:
					{
						valueToConvert = (T)(object)DateTime.Parse(value.ToString().Trim());
						return true;
					}
				case TypeCode.Decimal:
					{
						valueToConvert = (T)(object)Convert.ToDecimal(value);
						return true;
					}
				case TypeCode.Double:
					{
						valueToConvert = (T)(object)Convert.ToDouble(value);
						return true;
					}
				case TypeCode.Int16:
					{
						valueToConvert = (T)(object)Convert.ToInt16(value);
						return true;
					}
				case TypeCode.Int32:
					{
						valueToConvert = (T)(object)Convert.ToInt32(value);
						return true;
					}
				case TypeCode.Int64:
					{
						valueToConvert = (T)(object)Convert.ToInt64(value);
						return true;
					}
				case TypeCode.SByte:
					{
						valueToConvert = (T)(object)Convert.ToSByte(value);
						return true;
					}
				case TypeCode.Single:
					{
						valueToConvert = (T)(object)Convert.ToSingle(value);
						return true;
					}
				case TypeCode.String:
					{
						valueToConvert = (T)(object)Convert.ToString(value);
						return true;
					}
				case TypeCode.UInt16:
					{
						valueToConvert = (T)(object)Convert.ToUInt16(value);
						return true;
					}
				case TypeCode.UInt32:
					{
						valueToConvert = (T)(object)Convert.ToUInt32(value);
						return true;
					}
				case TypeCode.UInt64:
					{
						valueToConvert = (T)(object)Convert.ToUInt64(value);
						return true;
					}
#if !NETCOREAPP1_0
                case TypeCode.DBNull:
#endif
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

		public static string ToCode(this object o)
		{
			var response = new ObjectEmissionQuery(o).Execute();
			if (response.IsOk)
				return response.Text;
			return response.Message;
		}
		private static string getEnumFriendlyName(object source)
		{
			if (source == null)
				return string.Empty;
#if !NETCOREAPP1_0 && !PCL
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
					return ((DisplayAttribute) display).Name;
			}					
#endif
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
			}

			var val = To<string>(o);

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
			var customMapping = customMappings.FirstOrDefault(c => c.Type == typeof(T) && c.Values.Contains(value));

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