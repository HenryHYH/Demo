using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSdk
{
    public class ValuesRequest : BaseRequest<IList<string>>
    {
        public ValuesRequest()
            : base("api/values")
        {
        }
    }
}
