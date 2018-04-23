using Consul;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebConsumer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            IList<Uri> serverUrls = new List<Uri>();
            var consulClient = new ConsulClient(c => c.Address = new Uri("http://192.168.5.230:8500"));
            var services = consulClient.Agent.Services().Result.Response;
            foreach (var service in services)
            {
                var isTestApi = service.Value.Tags.Any(t => t == "A") &&
                                service.Value.Tags.Any(t => t == "B");

                if (isTestApi)
                {
                    var serviceUri = new Uri($"{service.Value.Address}:{service.Value.Port}");
                    serverUrls.Add(serviceUri);
                }
            }

            return serverUrls.Select(x => x.ToString());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
