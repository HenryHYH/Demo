using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.Infrastructure;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private const string KEY = "henry.services.show.time";
        private readonly ICapPublisher publisher;

        public PublishController(ICapPublisher publisher)
        {
            this.publisher = publisher;
        }

        [HttpGet("PublishMessage")]
        public async Task<IActionResult> PublishMessage([FromServices] AppDbContext dbContext)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                await publisher.PublishAsync(KEY, DateTime.Now);
                trans.Commit();
            }

            return Ok();
        }

        [NonAction]
        [CapSubscribe(KEY)]
        public void ReceiveMessage(DateTime dateTime)
        {
            Console.WriteLine("1-{0:yyyy-MM-dd HH:mm:ss.fff}", dateTime);
        }

        [NonAction]
        [CapSubscribe(KEY)]
        public void ReceiveMessage2(DateTime dateTime)
        {
            Console.WriteLine("2-{0:yyyy-MM-dd HH:mm:ss.fff}", dateTime);
        }
    }
}