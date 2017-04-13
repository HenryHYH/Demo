using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Model
    {
        [JsonProperty(PropertyName = "a", Order = 2)]
        public string A { get; set; }

        [JsonProperty(PropertyName = "b", Order = 1)]
        public string B { get; set; }

        //[JsonProperty(PropertyName = "c", Order = 3)]
        public int C { get; set; }

        [JsonProperty(Order = int.MaxValue)]
        public string Sign { get; set; }
    }
}
