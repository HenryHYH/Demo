using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Entities;

namespace WebApp.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class RestController : Controller
    {
        // GET: api/Rest
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new User[] {
                new User { Id = 1, Name = "[1]" },
                new User { Id = 2, Name = "[2]" }
            };
        }

        // GET: api/Rest/5
        [HttpGet("{id}", Name = "Get")]
        public User Get(int id)
        {
            return new User
            {
                Id = id,
                Name = $"[{id}]"
            };
        }

        // POST: api/Rest
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Rest/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
