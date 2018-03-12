using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Voodoo.Linq
{
    /// <summary>
    ///     Derived from http://msdn.microsoft.com/en-US/vstudio/bb894665.aspx
    /// </summary>
    public static class LinqHelper
    {
        private static MemberExpression getNestedExpressionProperty(Expression expression, string propertyName)
        {
            var parts = propertyName.Split('.');
            var length = parts.Length;

            return (length > 1)
                ? Expression.Property(
                    getNestedExpressionProperty(
                        expression,
                        parts.Take(length - 1)
                            .Aggregate((a, i) => a + "." + i)
                    ),
                    parts[length - 1])
                : Expression.Property(expression, propertyName);
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string ordering)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ordering == null) throw new ArgumentNullException(nameof(ordering));
            var parameters = new[] {Expression.Parameter(source.ElementType, "")};
            var orderings = ordering.Split(',');
            var methodAsc = "OrderBy";
            var methodDesc = "OrderByDescending";
            var type = typeof(T);
            var query = source.Expression;
            PropertyInfo property = null;
            foreach (var o in orderings)
            {
                var ascending = true;
                var expr = o.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if (expr.Count() > 1 && expr[1].ToUpper() == Strings.SortDirection.Descending)
                    ascending = false;

                var sort = expr[0];
                if (sort.Contains("."))
                {
                    var nestedProperties = sort.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
                    var nestedType = type;
                    foreach (var prop in nestedProperties)
                    {
                        foreach (var propertyInfo in nestedType.GetProperties())
                        {
                            if (propertyInfo.Name == prop)
                            {
                                property = propertyInfo;
                                break;
                            }
                        }
                        if (property == null)
                            throw new ArgumentException(
                                $"Could not find property {prop} on type {nestedType.Name} for expression {ordering}");
                        nestedType = property.PropertyType;
                    }
                }
                else
                    foreach (var propertyInfo in type.GetProperties())
                    {
                        if (propertyInfo.Name == sort)
                        {
                            property = propertyInfo;
                            break;
                        }
                    }

                if (property == null)
                    throw new Exception(
                        $"Could not sort on property {sort} on type {type.Name}, check the case perhaps."
                    );
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = getNestedExpressionProperty(parameter, sort);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                var method = ascending ? methodAsc : methodDesc;
                query = Expression.Call(typeof(Queryable), method, new[] {type, property.PropertyType}, query,
                    Expression.Quote(orderByExp));

                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }
            return source.Provider.CreateQuery<T>(query);
        }

        public static IQueryable Take(this IQueryable source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return
                source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Take", new[] {source.ElementType},
                    source.Expression, Expression.Constant(count)));
        }

        public static IQueryable Skip(this IQueryable source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return
                source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Skip", new[] {source.ElementType},
                    source.Expression, Expression.Constant(count)));
        }

        public static bool Any(this IQueryable source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return
                (bool)
                source.Provider.Execute(Expression.Call(typeof(Queryable), "Any", new[] {source.ElementType},
                    source.Expression));
        }

        public static int Count(this IQueryable source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return
                (int)
                source.Provider.Execute(Expression.Call(typeof(Queryable), "Count", new[] {source.ElementType},
                    source.Expression));
        }
    }
}