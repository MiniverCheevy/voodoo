using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using Voodoo.Infrastructure.Notations;

namespace Voodoo.Linq
{
    /// <summary>
    /// Derived from http://msdn.microsoft.com/en-US/vstudio/bb894665.aspx
    /// </summary>
    public static class LinqHelper
    {        
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string ordering)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (ordering == null) throw new ArgumentNullException("ordering");
            var parameters = new[] { Expression.Parameter(source.ElementType, "") };
            var orderings = ordering.Split(',');
            var methodAsc = "OrderBy";
            var methodDesc = "OrderByDescending";
            var type = typeof(T);
            var query = source.Expression;

            foreach (var o in orderings)
            {
                var ascending = true;
                var expr = o.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (expr.Count() > 1 && expr[1].ToUpper() == "DESC")
                    ascending = false;

                var sort = expr[0];
                var property = type.GetProperty(sort);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                var method = ascending ? methodAsc : methodDesc;
                query = Expression.Call(typeof(Queryable), method, new Type[] { type, property.PropertyType }, query, Expression.Quote(orderByExp));                

                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }
            return source.Provider.CreateQuery<T>(query);
        }

            public static IQueryable Take(this IQueryable source, int count)
            {
                if (source == null) throw new ArgumentNullException("source");
                return
                    source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Take", new[] { source.ElementType },
                        source.Expression, Expression.Constant(count)));
            }

            public static IQueryable Skip(this IQueryable source, int count)
            {
                if (source == null) throw new ArgumentNullException("source");
                return
                    source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Skip", new[] { source.ElementType },
                        source.Expression, Expression.Constant(count)));
            }

           
            public static bool Any(this IQueryable source)
            {
                if (source == null) throw new ArgumentNullException("source");
                return
                    (bool)
                        source.Provider.Execute(Expression.Call(typeof(Queryable), "Any", new[] { source.ElementType },
                            source.Expression));
            }

            public static int Count(this IQueryable source)
            {
                if (source == null) throw new ArgumentNullException("source");
                return
                    (int)
                        source.Provider.Execute(Expression.Call(typeof(Queryable), "Count", new[] { source.ElementType },
                            source.Expression));
            }
        }
    }
