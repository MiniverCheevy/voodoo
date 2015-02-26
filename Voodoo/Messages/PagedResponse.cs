using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages.Paging;

namespace Voodoo.Messages
{
    public class PagedResponse<TQueryResult> : ListResponse<TQueryResult>, IResponse where TQueryResult : new()
    {
        public PagedResponse()
        {
            State = new GridState();
        }

        public PagedResponse(IGridState state)
        {
            State = state;
            State.Map(state);
        }
        public void From<TIn, TOut>(PagedResponse<TIn> source, Func<TIn, TOut> transform)
            where TIn: class, new()
            where TOut : TQueryResult
        {
            State = source.State;            
            State.Map(source.State);
            var transformed = source.Data.Select(transform).ToList();
            foreach (var item in transformed)
                Data.Add(item);            
        }

        public IGridState State { get; set; }
    }
}