using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Web.Framework.Datasource
{
    public class DatasourceRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DatasourceRequest()
        {
            PageIndex = 1;
            PageSize = 20;
        }
    }
}
