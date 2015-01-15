using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Core.Domain.Localization
{
    public class Language : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
