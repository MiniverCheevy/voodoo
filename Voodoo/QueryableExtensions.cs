using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Voodoo.Linq;
using Voodoo.Messages;
using Voodoo.Messages.Paging;
using Voodoo;

namespace Voodoo
{
    public static class QueryableExtensions
    {
        public static IQueryable<TQueryResult> OrderByDescending<TQueryResult>(this IQueryable<TQueryResult> query,
            string sortExpression) where TQueryResult : class
        {
            return query.OrderByDynamic(string.Format("{0} {1}", sortExpression, Constants.SortDirection.Descending));
        }

        public static PagedResponse<TObject> PagedResult<TObject>(this IQueryable<TObject> source, IGridState paging)
            where TObject : class, new()
        {
            return PagedResult(source, paging, x => x);
        }

        public static PagedResponse<TOut> PagedResult<TIn, TOut>(this IQueryable<TIn> source, IGridState paging,
            Expression<Func<TIn, TOut>> expression) where TIn : class where TOut : class, new()
        {

            var sortMember = paging.SortMember ?? paging.DefaultSortMember;

            source = !string.IsNullOrEmpty(sortMember)
                ? source.OrderByDynamic(string.Format("{0} {1}", sortMember, paging.SortDirection))
                : source.OrderBy(c => true);

            var total = DynamicQueryable.Count(source);
            paging.TotalRecords = total;
            var state = new GridState(paging);
            var skip = (state.PageNumber - 1)*state.PageSize;
            skip = skip < 0 ? 0 : skip;
            var take = state.PageSize;
            var page = take == int.MaxValue
                ? source.Select(expression)
                : DynamicQueryable.Skip(source.Select(expression), skip).Take(take);

            //var list = page.Cast<TOut>().ToList();
            var list = new List<TOut>();
            foreach (var item in page)
            {
                list.Add(item.To<TOut>());
            }

            var result = new PagedResponse<TOut>(state) {State = {TotalRecords = total}};
            result.Data.AddRange(list);
            return result;
        }

        public static string GetName<T>(this T type, Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body != null)
                return body.Member.Name;

            var op = ((UnaryExpression) expression.Body).Operand;
            return ((MemberExpression) op).Member.Name;
        }

        [DebuggerStepThrough]
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        [DebuggerStepThrough]
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            if (first == null)
                return second;
            return first.Compose(second, Expression.And);
        }

        [DebuggerStepThrough]
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
    }
}