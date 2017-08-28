using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// User demo
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new[] { "Hello", "World" };
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return $"Get_{id}";
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public string Post([FromBody]string value)
        {
            return $"Post_{value}";
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public string Put(int id, [FromBody]string value)
        {
            return $"Put_{id}_{value}";
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Id</param>
        public string Delete(int id)
        {
            return $"Delete_{id}";
        }
    }
}