namespace FW.Web.Framework.Datasource
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DatasourceRequest
    {
        #region Constructors

        public DatasourceRequest()
        {
            PageIndex = 1;
            PageSize = 20;
        }

        #endregion Constructors

        #region Properties

        public int PageIndex
        {
            get; set;
        }

        public int PageSize
        {
            get; set;
        }

        #endregion Properties
    }
}