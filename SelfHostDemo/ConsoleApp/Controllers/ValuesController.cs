﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ConsoleApp.Services;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }

        #endregion
    }
}
