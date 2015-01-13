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

        public string ResourceKey { get; set; }

        public string ResourceValue { get; set; }
    }
}
