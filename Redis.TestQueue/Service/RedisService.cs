using StackExchange.Redis;

namespace Redis.TestQueue.Service
{
    public class RedisService
    {
        public static IDatabase _RedisDb { private set; get; }
        public static IServer _RedisServer { private set; get; }

        public void Start()
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            
            _RedisDb = redis.GetDatabase();

            _RedisServer = redis.GetServer("localhost:6379");
        }

        public void RightPush(string key, string value)
        {
            _RedisDb.ListRightPush(key, value);
        }
    }
}
