using System;
using System.Collections.Generic;
using System.Web.Http;
using ConsoleApp.Models.Values;
using ConsoleApp.Services.Logging;

namespace ConsoleApp.Controllers
{
    public class ValuesController : ApiController
    {
        #region Fields

        private readonly ILogger logger;

        #endregion

        #region Ctor

        public ValuesController(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Methods

        // GET api/values 
        //[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        //[HttpGet]
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST api/values 
        //[HttpPost]
        public void Post([FromBody]ValueModel value)
        {
            logger.Debug(string.Format("[Post] {0}", value.Value));
        }

        // PUT api/values/5 
        //[HttpPut]
        public void Put(int id, [FromBody]ValueModel value)
        {
            logger.Debug(string.Format("[Put] {0} - {1}", id, value.Value));
        }

        // DELETE api/values/5
        //[HttpDelete]
        public void Delete(int id)
        {
            logger.Debug(string.Format("[Delete] {0}", id));
        }

        #endregion
    }
}
