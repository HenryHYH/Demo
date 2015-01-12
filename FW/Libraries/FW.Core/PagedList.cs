namespace FW.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core;

    public class PagedList<T> : List<T>, IPagedList<T>
    {
        #region Constructors

        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            this.TotalRecords = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                this.TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageSize * pageIndex).Take(pageSize).ToList());
        }

        #endregion Constructors

        #region Properties

        public bool HasNextPage
        {
            get { return PageIndex + 1 < TotalPages; }
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 0; }
        }

        public int PageIndex
        {
            get; private set;
        }

        public int PageSize
        {
            get; private set;
        }

        public int TotalRecords
        {
            get; private set;
        }

        public int TotalPages
        {
            get; private set;
        }

        #endregion Properties
    }
}