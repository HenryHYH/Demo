namespace FW.Web.Framework.Datasource
{
    using System.Collections.Generic;

    using FW.Web.Framework.UI.Pagination;

    public class DatasourceResult<T>
    {
        #region Properties

        public IList<T> Data
        {
            get; set;
        }

        public Pager Pager
        {
            get; set;
        }

        #endregion Properties
    }
}