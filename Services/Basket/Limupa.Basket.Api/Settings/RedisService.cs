using StackExchange.Redis;
using System.Data;

namespace Limupa.Basket.Api.Settings
{
    public class RedisService
    {
        public string host { get; set; }
        public int port { get; set; }

        private ConnectionMultiplexer connectionMultiplexer;

        public RedisService(string host,int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Connect() => connectionMultiplexer = ConnectionMultiplexer.Connect($"{host}:{port}");
        public IDatabase GetDb(int db = 1) => connectionMultiplexer.GetDatabase(0);
    }
}
