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
            State = new GridState(state);
        }


        public IGridState State { get; set; }
    }
}