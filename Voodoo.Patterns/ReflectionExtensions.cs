using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo.Logging;
#if (DNXCore)
using System.Reflection.IntrospectionExtensions;
#endif
namespace Voodoo
{
    public static class ReflectionExtensions
    {
#if DNX40
        public static Type GetTypeInfo(this Type t)
        {
            return t;
        }

        public static PropertyInfo GetDeclaredProperty(this Type type,string name)
        {
        return type.GetProperty(name);
        }
#endif

        public static bool IsEnumerable(this Type t)
        {
            return typeof (IEnumerable).IsAssignableFrom(t);
        }

        public static bool IsScalar(this Type t)
        {
            const string types = "string,guid,datetime,timespan,decimal";
            if (t.Name.ToLower().Contains("nullable"))
                return true;

            if (types.Contains(t.Name.ToLower()))
                return true;

            if (t.GetTypeInfo().IsPrimitive)
                return true;

            if (t.GetTypeInfo().IsEnum)
                return true;

            return false;
        }

#if !PCL
        public static Type[] GetTypesSafetly(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(c=>c != null).ToArray();

            }
            catch (ReflectionTypeLoadException rtl)
            {
                LogManager.Log(string.Format("error loading types from {0}", assembly.FullName));
                return rtl.Types.Where(c => c != null).ToArray();
            }
        }
#endif

        public static string GetTypeNameWithoutGenericArguments(this Type type)
        {
            if (!type.GetGenericArguments().Any())
                return null;

            var index = 0;
            var testedTypeName = type.Name;
            index = testedTypeName.IndexOf("`");
            if (index == -1)
                return null;
            testedTypeName = testedTypeName.Substring(0, index);
            return testedTypeName;
        }

        public static string GetTypeFullNameWithoutGenericArguments(this Type type)
        {
            if (!type.GetGenericArguments().Any())
                return null;

            var index = 0;
            var testedTypeName = type.FullName;
            index = testedTypeName.IndexOf("`");
            if (index == -1)
                return null;
            testedTypeName = testedTypeName.Substring(0, index);
            return testedTypeName;
        }
#if (!PCL)
        private static bool IsGenericTypeDirectlyInheritedFromOtherGenericType(this Type testedType,
            Type possibleBaseType)
        {
            var testedTypeIsNull = testedType == null;
            var possibleBaseTypeIsNull = possibleBaseType == null;
            var testTypeHasBaseType =  testedType.GetTypeInfo().BaseType != null ;
            var testedTypeHasGenericArguments = testedType.GetTypeInfo().BaseType.GetGenericArguments().Any() ;
            var possibleBaseTypeHasGenericArguments = possibleBaseType.GetGenericArguments().Any();

            if (testedTypeIsNull || possibleBaseTypeIsNull || !testTypeHasBaseType || ! testedTypeHasGenericArguments || 
                !possibleBaseTypeHasGenericArguments)                
                return false;

            var left = testedType.GetTypeInfo().BaseType;
            var right = possibleBaseType;

            return left.Name == right.Name && left.Namespace == right.Namespace &&
                   left.GetGenericArguments().Count() == right.GetGenericArguments().Count();

            //TODO: move to shares common anscestor
            //var baseTypeName = possibleBaseType.GetTypeFullNameWithoutGenericArguments();

            //var match = false;

            //while (testedType != null)
            //{
            //    var typeName = testedType.GetTypeFullNameWithoutGenericArguments();
            //    if (baseTypeName == typeName)
            //    {
            //        match = true;
            //        break;
            //    }
            //    testedType = testedType.BaseType;
            //}

            //return match;

            //if (!match)
            //    return false;

            //    var baseTypes = possibleBaseType.GetGenericArguments();
            //    var testTypes = testedType.GetGenericArguments();

            //    if (baseTypes.Count() != testTypes.Count())
            //        return false;

            //    var index = 0;
            //    foreach (var type in testTypes)
            //    {
            //        var testedChild = type;
            //        var testedBase = baseTypes[0];
            //        while (testedChild != null)
            //        {
            //            if (testedChild == testedBase)
            //                break;
            //            testedChild = testedChild.BaseType;
            //        }
            //        return false;
            //    }
            //    return true;            
        }
#endif
        public static List<KeyValuePair<Type, string>> GetParameterDictionary(this MethodInfo methodInfo)
        {
            var result = new List<KeyValuePair<Type, string>>();
            foreach (var info in methodInfo.GetParameters())
            {
                result.Add(new KeyValuePair<Type, string>(info.ParameterType, info.Name));
            }
            return result;
        }

        public static string GetParametersForCodeGeneration(this MethodInfo methodInfo)
        {
            var result = string.Empty;
            foreach (var info in methodInfo.GetParameters())
            {
                result += info.ParameterType.FixUpTypeName();
                result += " ";
                result += info.Name;
                result += ", ";
            }

            result = result.TrimEnd(' ').TrimEnd(',');
            return result;
        }

        /// <summary>
        ///     convert scalar Type names into compilable c# type names
        /// </summary>
        public static string FixUpScalarTypeName(this Type t)
        {
            var type = t.Name;
            type = type.Replace("System.", "");

            switch (type)
            {
                case "String":
                    return "string";
                case "Byte":
                    return "byte";
                case "Byte[]":
                    return "byte[]";
                case "Int16":
                    return "short";
                case "Int32":
                    return "int";
                case "Int64":
                    return "long";
                case "Char":
                    return "char";
                case "Single":
                    return "float";
                case "Double":
                    return "double";
                case "Boolean":
                    return "bool";
                case "Decimal":
                    return "decimal";
                case "SByte":
                    return "sbyte";
                case "UInt16":
                    return "ushort";
                case "UInt32":
                    return "uint";
                case "UInt64":
                    return "ulong";
                case "Object":
                    return "object";
                case "Void":
                    return "void";
                default:

                    return type;
            }
        }

        /// <summary>
        ///     /// convert Type name into compilable c# type names
        /// </summary>
        public static string FixUpTypeName(this Type type)
        {
            var result = type.FixUpScalarTypeName();
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                result = string.Format("{0}?", Nullable.GetUnderlyingType(type).FixUpScalarTypeName());
            }

            else if (type.GetTypeInfo().IsGenericType)
            {
                var inner = string.Empty;
                foreach (var t in type.GetGenericArguments())
                {
                    if (t.GetTypeInfo().IsGenericType)
                    {
                        var outer1 = t.GetGenericTypeDefinition().Name;
                        var ary1 = outer1.Split(@"`".ToCharArray());
                        outer1 = ary1[0];

                        var inner1 = string.Empty;
                        foreach (var t1 in t.GetGenericArguments())
                        {
                            inner1 += t1.Name;
                            inner1 += ",";
                        }
                        inner1 = inner1.TrimEnd(",".ToCharArray());
                        inner += string.Format("{1}<{0}>", inner1, outer1);
                    }
                    else
                    {
                        inner += t.Name;
                        inner += ",";
                    }
                }
                inner = inner.TrimEnd(",".ToCharArray());
                var outer = type.GetGenericTypeDefinition().Name;
                var ary = outer.Split(@"`".ToCharArray());
                outer = ary[0];
                result = string.Format("{1}<{0}>", inner, outer);
            }
            else
            {
                return result;
            }
            return result;
        }

        public static string GetMethodName(this MethodBase input)
        {
            return input.Name;
        }

        public static bool DoesImplementInterfaceOf(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().Contains(interfaceType);
        }

        public static bool IsGenericCollectionTypeOf(this Type type, Type typeDefinition)
        {
            if (type.GetTypeInfo().IsGenericType)
            {
                var collectionTypeInterfaces = new[] {typeof (IEnumerable), typeof (IList), typeof (ICollection)};
                var isCollectionType = type.GetInterfaces().Intersect(collectionTypeInterfaces).Any();
                var canConstructTypeDefinition =
                    type.GetGenericArguments().Any(c => c.GetInterfaces().Contains(typeDefinition));

                if (isCollectionType && canConstructTypeDefinition)
                    return true;
            }
            return false;
        }
    }
}