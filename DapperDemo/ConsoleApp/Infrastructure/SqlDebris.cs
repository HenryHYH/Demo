using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public class SqlDebris
    {
        public string Where { get; set; }
        #region 查询
        public string From { get; set; }
        public string Select { get; set; }
        public string Order { get; set; }
        public int Skip { get; set; }
        public int? Take { get; set; }
        #endregion
        #region 修改
        public string Set { get; set; }
        public string Update { get; set; }
        #endregion
        #region 新增

        public string InsertTable { get; set; }
        public string Columns { get; set; }
        public string Values { get; set; }

        #endregion
    }
}
