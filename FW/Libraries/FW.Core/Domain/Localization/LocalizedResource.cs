using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Core.Domain.Localization
{
    public class LocalizedResource : BaseEntity
    {
        public string Language { get; set; }

        public string Key { get; set; }

        public string Resource { get; set; }
    }
}
