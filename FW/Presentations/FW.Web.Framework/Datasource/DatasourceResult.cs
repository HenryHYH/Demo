using FW.Web.Framework.UI.Pagination;
using System.Collections.Generic;

namespace FW.Web.Framework.Datasource
{
    public class DatasourceResult<T>
    {
        public Pager Pager { get; set; }

        public IList<T> Data { get; set; }
    }
}
