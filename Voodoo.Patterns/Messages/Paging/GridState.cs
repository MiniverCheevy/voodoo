using System;

namespace Voodoo.Messages.Paging
{
    public class GridState : IGridState
    {
        public GridState()
        {
        }

        public GridState(IGridState state = null)
        {
            applyPaging(state);
            applySorting(state);
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

        private void applySorting(IGridState sorting)
        {
            if (sorting == null)
                return;

            DefaultSortMember = sorting.DefaultSortMember;
            SortMember = string.IsNullOrEmpty(sorting.SortMember) ? sorting.DefaultSortMember : sorting.SortMember;
            SortDirection = string.IsNullOrEmpty(sorting.SortDirection)
                ? Strings.SortDirection.Ascending
                : sorting.SortDirection;
        }

        private void applyPaging(IGridState paging)
        {
            if (paging != null)
            {
                PageNumber = paging.PageNumber > 0 ? paging.PageNumber : 1;
                PageSize = paging.PageSize > 0 ? paging.PageSize : 10;
                TotalRecords = paging.TotalRecords;
                TotalPages = Math.Ceiling(TotalRecords.To<decimal>() / PageSize.To<decimal>()).To<int>();
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