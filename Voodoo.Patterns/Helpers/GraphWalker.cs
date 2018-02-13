using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;

namespace Voodoo.Helpers
{
    public class GraphWalkerSettings
    {
        /// <summary>
        /// Return both T and Nullable<T> in the list of distinct types
        /// </summary>
        public bool TreatNullableTypesAsDistict { get; set; }

        /// <summary>
        /// Include Primitives in addition to string, date and guid
        /// </summary>
        public bool IncludeScalarTypes { get; set; }
    }



    public class GraphWalker
    {
        private readonly HashSet<Type> distinctTypes = new HashSet<Type>();
        private readonly GraphWalkerSettings settings = new GraphWalkerSettings();
        private readonly Type[] types;

        public GraphWalker(params Type[] types)
        {
            if (types != null)
                this.types = new Type[] { typeof(DayOfWeek?) }.Union(types.Distinct().OrderBy(c => c.Name).ToArray()).ToArray(); ;
        }

        public GraphWalker(GraphWalkerSettings settings, params Type[] types) : this(types)
        {
            this.settings = settings;
        }

        public HashSet<Type> GetDistinctTypes()
        {
            foreach (var type in types)
            {
                read(type);
            }
            var orderedResult = new HashSet<Type>();
            var names = new HashSet<string>();
            foreach (var item in distinctTypes.Distinct().OrderBy(c => c.Name))
            {
                if (!names.Contains(item.FullName))
                {
                    names.Add(item.FullName);
                    orderedResult.Add(item);
                }
            }
            return orderedResult;
        }

        private void read(Type type)
        {
            var isNullable = type.IsNullable();
            var isScalar = type.IsScalar();
            var isEnum = type.IsEnum();           

            if (isScalar && !settings.IncludeScalarTypes && !isEnum && !isNullable)
                return;

            if (isNullable && !settings.TreatNullableTypesAsDistict)
            { 
                type = type.GetGenericArgumentsList().First();
                isNullable = type.IsNullable();
                isScalar = type.IsScalar();
                isEnum = type.IsEnum();
            }
            else if (type.IsGenericType())
            {
                foreach (var argument in type.GetGenericArgumentsList())
                {
                    read(argument);
                }
            }

            if (type.FullName.StartsWith("System.") && !isScalar && !isNullable)
                return;

            if (distinctTypes.Contains(type))
                return;

            if (isEnum)
            {
                distinctTypes.Add(type);
                return;
            }
            if (isNullable && settings.TreatNullableTypesAsDistict)
            {
                distinctTypes.Add(type);
                return;
            }
            if (isScalar && settings.IncludeScalarTypes)
            {
                distinctTypes.Add(type);
                return;
            }
            else if (isScalar)
                return;

            distinctTypes.Add(type);

            foreach (var property in type.GetPropertiesList())
            {
                read(property.PropertyType);
            }
        }




    }
}
