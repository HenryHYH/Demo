namespace FW.Web.Framework.UI.Pagination
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Pager
    {
        #region Constructors

        public Pager()
            : this(1, 20)
        {
        }

        public Pager(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        #endregion Constructors

        #region Properties

        public bool HasNext
        {
            get { return PageIndex < TotalPages; }
        }

        public bool HasPrevious
        {
            get { return PageIndex > 1; }
        }

        public int PageIndex
        {
            get; private set;
        }

        public int PageSize
        {
            get; private set;
        }

        public int TotalPages
        {
            get
            {
                return TotalRecords / PageSize +
                    ((TotalRecords % PageSize == 0) ? 0 : 1);
            }
        }

        public int TotalRecords
        {
            get; set;
        }

        #endregion Properties
    }
}