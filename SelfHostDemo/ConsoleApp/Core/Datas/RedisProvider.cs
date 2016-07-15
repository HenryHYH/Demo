using ConsoleApp.Core.Settings;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace ConsoleApp.Core.Datas
{
    public class RedisProvider
    {
        public static ICacheClient GetClient(DataSettings dataSettings)
        {
            ISerializer serializer = new NewtonsoftSerializer();
            var conn = ConnectionMultiplexer.Connect(dataSettings.RawSettings["RedisConnectionString"]);

            return new StackExchangeRedisCacheClient(conn, serializer, int.Parse(dataSettings.RawSettings["RedisDbIndex"]));
        }
    }
}
