using Newtonsoft.Json;
using System;

namespace HelloWeb.MessageSystem.MessageQueue
{
    public class TestData
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CTime { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
