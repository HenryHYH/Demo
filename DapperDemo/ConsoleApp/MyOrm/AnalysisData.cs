using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.MyOrm
{
    public class AnalysisData
    {
        public AnalysisData()
        {
            StackList = new List<string>();
            ParamList = new Dictionary<string, object>();
        }

        public AnalysisTable Table { get; set; }

        public IList<string> StackList { get; set; }

        public IDictionary<string, object> ParamList { get; set; }
    }
}
