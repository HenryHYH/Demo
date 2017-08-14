using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebApp.Tests
{
    public class BookControllerTest
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public BookControllerTest()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestIndexAsync()
        {
            var response = await client.GetAsync("/book");
            response.EnsureSuccessStatusCode();

            var message = await response.Content.ReadAsStringAsync();

            Assert.Equal("1", message);
        }
    }
}
