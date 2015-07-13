using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Voodoo
{
#if (PCL)
    public enum TypeCode
    {
        Empty = 0,
        Object = 1,
        DBNull = 2,
        Boolean = 3,
        Char = 4,
        SByte = 5,
        Byte = 6,
        Int16 = 7,
        UInt16 = 8,
        Int32 = 9,
        UInt32 = 10,
        Int64 = 11,
        UInt64 = 12,
        Single = 13,
        Double = 14,
        Decimal = 15,
        DateTime = 16,
        String = 18
    }
#endif
    public static class PCLExtensions
    {
#if (PCL)
        public static object ParseEnum(this Type enumType, string value)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");
            
            if (!enumType.IsEnum())
                throw new ArgumentException("enumType must be an enum");
            
            value = value.Trim();
            if (value.Length == 0)
            {
                throw new ArgumentNullException("enumType");
            }

            var rawValues = Enum.GetValues(enumType);
            var values = new Dictionary<string,object>();

            foreach (var rawValue in rawValues)
            {               
                if (value.ToLower() == rawValue.ToString().ToLower())
                    return rawValue;

                if (((int) rawValue).ToString() == value)
                    return rawValue;
            };
           throw new ArgumentException("Could not parse value");
        }

        public static bool IsAssignableFrom(this Type typeFrom, Type typeTo)
        {
            return typeFrom.GetTypeInfo().IsAssignableFrom(typeTo.GetTypeInfo());
        }

        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static PropertyInfo GetProperty(this Type t, string name)
        {
            return t.GetTypeInfo().GetDeclaredProperty(name);
        }

        private static readonly Dictionary<Type, TypeCode> typeCodes =
            new Dictionary<Type, TypeCode>
            {
                {typeof (bool), TypeCode.Boolean},
                {typeof (char), TypeCode.Char},
                {typeof (byte), TypeCode.Byte},
                {typeof (short), TypeCode.Int16},
                {typeof (int), TypeCode.Int32},
                {typeof (long), TypeCode.Int64},
                {typeof (sbyte), TypeCode.SByte},
                {typeof (ushort), TypeCode.UInt16},
                {typeof (uint), TypeCode.UInt32},
                {typeof (ulong), TypeCode.UInt64},
                {typeof (float), TypeCode.Single},
                {typeof (double), TypeCode.Double},
                {typeof (DateTime), TypeCode.DateTime},
                {typeof (decimal), TypeCode.Decimal},
                {typeof (string), TypeCode.String}
            };

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }


        public static TypeCode GetTypeCode(this Type type)
        {
            var response = TypeCode.Empty;
            if (type == null)
                return response;

            if (typeCodes.ContainsKey(type))
                return typeCodes[type];

            response = TypeCode.Object;

            return response;
        }

        public static Type[] GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces.ToArray();
        }

#endif
    }

}
