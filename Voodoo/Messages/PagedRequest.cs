using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Voodoo.Messages.Paging;

namespace Voodoo.Messages
{
    public abstract class PagedRequest : Request, IGridState
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected PagedRequest()
        {
            SortMember = DefaultSortMember;
            PageNumber = 1;
            PageSize = 10;
        }

        public abstract string DefaultSortMember { get; }
        public string SortDirection { get; set; }
        public bool ResetPaging { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortMember { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}