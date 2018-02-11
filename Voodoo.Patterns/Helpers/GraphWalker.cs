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
            this.types = types;
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

            return distinctTypes;
        }

        private void read(Type type)
        {

            if (type.IsScalar() && !settings.IncludeScalarTypes)
                return;
            if (isNullable(type) && !settings.TreatNullableTypesAsDistict)
                type = type.GetGenericArguments().First();
            if (distinctTypes.Contains(type))
                return;

            distinctTypes.Add(type);

            foreach (var property in type.GetProperties())
            {
                var propertyType = property.PropertyType;
                if (propertyType.IsScalar() && !settings.IncludeScalarTypes)
                    continue;

                read(propertyType);
            }
        }

        private bool isNullable(Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);



    }
}
