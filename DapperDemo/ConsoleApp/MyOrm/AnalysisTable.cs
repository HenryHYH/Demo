using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.MyOrm
{
    public class AnalysisTable
    {
        public AnalysisTable()
        {
            LeftJoins = new List<AnalysisTable>();
        }

        public string RName { get; set; }

        public string Name { get; set; }

        public Type TableType { get; set; }

        public IList<AnalysisTable> LeftJoins { get; set; }
    }
}
