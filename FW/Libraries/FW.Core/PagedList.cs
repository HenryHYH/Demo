namespace FW.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core;
    using System.Linq.Expressions;

    public class PagedList<T> : List<T>, IPagedList<T>
        where T : BaseEntity
    {
        #region Fields

        private static readonly string[] PaginationPrerequisiteMehods = new[] { "OrderBy", "OrderByDescending" };

        #endregion

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
            var methodName = ((MethodCallExpression)source.Expression).Method.Name;
            if (!Array.Exists(PaginationPrerequisiteMehods, s => s.Equals(methodName, StringComparison.InvariantCulture)))
            {
                source = source.OrderByDescending(x => x.Id);
            }
            this.AddRange(source.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            get;
            private set;
        }

        public int PageSize
        {
            get;
            private set;
        }

        public int TotalPages
        {
            get;
            private set;
        }

        public int TotalRecords
        {
            get;
            private set;
        }

        #endregion Properties
    }
}