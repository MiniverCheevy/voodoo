using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Voodoo
{
    /// <summary>
    /// http://stackoverflow.com/questions/35370384/how-to-get-declared-and-inherited-members-from-typeinfo
    /// </summary>
    public static class TypeInfoExtensions
    {
        public static IEnumerable<ConstructorInfo> GetAllConstructors(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredConstructors);

        public static IEnumerable<EventInfo> GetAllEvents(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredEvents);

        public static IEnumerable<FieldInfo> GetAllFields(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredFields);

        public static IEnumerable<MemberInfo> GetAllMembers(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredMembers);

        public static IEnumerable<MethodInfo> GetAllMethods(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredMethods);

        public static IEnumerable<TypeInfo> GetAllNestedTypes(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredNestedTypes);

        public static IEnumerable<PropertyInfo> GetAllProperties(this TypeInfo typeInfo)
            => GetAll(typeInfo, ti => ti.DeclaredProperties);

        private static IEnumerable<T> GetAll<T>(TypeInfo typeInfo, Func<TypeInfo, IEnumerable<T>> accessor)
        {
            while (typeInfo != null)
            {
                foreach (var t in accessor(typeInfo))
                {
                    yield return t;
                }

                typeInfo = typeInfo.BaseType?.GetTypeInfo();
            }
        }
    }
}