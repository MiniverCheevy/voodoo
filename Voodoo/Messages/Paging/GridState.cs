using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Voodoo.Messages.Paging
{
    public class GridState : IGridState
    {
        public GridState()
        {
        }

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GridState(IGridState state = null)
        {
            ApplyPaging(state);
            ApplySorting(state);
            resetPagingIfNeeded(state);
        }

        public virtual int TotalPages { get; set; }
        public virtual int PageNumber { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int TotalRecords { get; set; }
        public virtual string SortMember { get; set; }
        public virtual string SortDirection { get; set; }
        public virtual string DefaultSortMember { get; set; }
        public virtual bool ResetPaging { get; set; }

        protected void ApplySorting(IGridState sorting)
        {
            if (sorting == null)
                return;

            DefaultSortMember = sorting.DefaultSortMember;
            SortMember = String.IsNullOrEmpty(sorting.SortMember) ? sorting.DefaultSortMember : sorting.SortMember;
            SortDirection = String.IsNullOrEmpty(sorting.SortDirection)
                ? Strings.SortDirection.Ascending
                : sorting.SortDirection;
        }

        protected void ApplyPaging(IGridState paging)
        {
            if (paging != null)
            {
                PageNumber = paging.PageNumber > 0 ? paging.PageNumber : 1;
                PageSize = paging.PageSize > 0 ? paging.PageSize : 10;
                TotalRecords = paging.TotalRecords;
                TotalPages = Math.Ceiling(TotalRecords.To<decimal>()/PageSize.To<decimal>()).To<int>();
                ResetPaging = paging.ResetPaging;
                if (PageNumber <= 0)
                    PageNumber = 1;
                if (PageNumber > TotalPages)
                    PageNumber = TotalPages;
            }
            else
            {
                PageNumber = 1;
                PageSize = 10;
            }
        }

        private void resetPagingIfNeeded(IGridState state)
        {
            if (state != null && state.ResetPaging)
                PageNumber = 1;
        }
    }
}