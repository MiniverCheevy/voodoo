using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Voodoo.Linq;
using Voodoo.Messages;
using Voodoo.Messages.Paging;

namespace Voodoo
{
    public static class QueryableExtensions
    {
        public static IGridState Map(this IGridState target, IGridState source)
        {
            target.PageNumber = source.PageNumber;
            target.PageSize = source.PageSize;
            target.TotalRecords = source.TotalRecords;
            target.SortMember = source.SortMember;
            target.SortDirection = source.SortDirection;
            target.ResetPaging = source.ResetPaging;
            target.TotalPages = source.TotalPages;
            return target;
        }

        public static IQueryable<TQueryResult> OrderByDescending<TQueryResult>(this IQueryable<TQueryResult> query,
            string sortExpression) where TQueryResult : class
        {
            return query.OrderByDynamic(string.Format("{0} {1}", sortExpression, Strings.SortDirection.Descending));
        }

        public static PagedResponse<TObject> ToPagedResponse<TObject>(this IQueryable<TObject> source, IGridState paging)
            where TObject : class, new()
        {
            return ToPagedResponse(source, paging, x => x);
        }

        public static PagedResponse<TOut> ToPagedResponse<TIn, TOut>(this IQueryable<TIn> source, IGridState paging,
            Expression<Func<TIn, TOut>> transform) where TIn : class where TOut : class, new()
        {
            IQueryable page;
            var state = buildPagedQuery(source, paging, transform, out page);

            var list = page.ToArray<TOut>();

            var result = new PagedResponse<TOut>(state);

            result.Data.AddRange(list);
            return result;
        }

        private static IGridState buildPagedQuery<TIn, TOut>(IQueryable<TIn> source, IGridState paging,
            Expression<Func<TIn, TOut>> expression, out IQueryable page) where TIn : class where TOut : class, new()
        {
            var sortMember = paging.SortMember ?? paging.DefaultSortMember;

            source = !string.IsNullOrEmpty(sortMember)
                ? source.OrderByDynamic(string.Format("{0} {1}", sortMember, paging.SortDirection))
                : source.OrderBy(c => true);

            paging.TotalRecords = LinqHelper.Count(source);
            paging.TotalPages = Math.Ceiling(paging.TotalRecords.To<decimal>()/paging.PageSize.To<decimal>()).To<int>();
            var state = paging.Map(new GridState(paging));


            var skip = (state.PageNumber - 1)*state.PageSize;
            skip = skip < 0 ? 0 : skip;
            var take = state.PageSize;
            var data = take == int.MaxValue ? source.ToArray() : source.Skip(skip).Take(take).ToArray();
            var transformed = data.AsQueryable().Select(expression).ToArray<TOut>();
            page = transformed.AsQueryable();
            return state;
        }

        public static PagedResponse<Grouping<TSource>> GroupBy<TSource, TKey>(this PagedResponse<TSource> source,
            Func<TSource, TKey> keySelector) where TSource : class, new()
        {
            var response = new PagedResponse<Grouping<TSource>>(source.State);
            var grouped = source.Data.GroupBy(keySelector);
            response.Data =
                grouped.Select(c => new Grouping<TSource> {Name = c.Key.To<string>(), Data = c.ToList()}).ToList();
            return response;
        }

        public static string GetName<T>(this T type, Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body != null)
                return body.Member.Name;

            var op = ((UnaryExpression) expression.Body).Operand;
            return ((MemberExpression) op).Member.Name;
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            if (first == null)
                return second;
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            if (first == null)
                return second;
            return first.Compose(second, Expression.Or);
        }

        /// <summary>
        ///     http://microsoftnlayerapp.codeplex.com/
        /// </summary>
        public class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
                Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }

#if ! DNX40
        public static async Task<PagedResponse<TObject>> ToPagedResponseAsync<TObject>(this IQueryable<TObject> source,
            IGridState paging) where TObject : class, new()
        {
            return await Task.Run(() => ToPagedResponse(source, paging, c => c));
        }

        public static async Task<PagedResponse<TOut>> ToPagedResponseAsync<TIn, TOut>(this IQueryable<TIn> source,
            IGridState paging, Expression<Func<TIn, TOut>> transform) where TIn : class where TOut : class, new()
        {
            return await Task.Run(() => ToPagedResponse(source, paging, transform));
        }

#endif
    }
}