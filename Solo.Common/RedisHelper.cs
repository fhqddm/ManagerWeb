using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Solo.Common
{
    public class RedisHelper
    {
        public readonly IDatabase _db;
        public readonly ConnectionMultiplexer _redis;
        public RedisHelper(string redisConnectStr)
        {
            _redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=123456,allowAdmin=true");
            _db = _redis.GetDatabase();
        }
        public bool SetRedisData<T>(string key,T obj)
        {
            string json = JsonConvert.SerializeObject(obj);//序列化
            return _db.StringSet(key, json);
        }

        public T GetRedisData<T>(string key)
        {
            string str = _db.StringGet(key);
            if (str == null)
            {
                str = "";
            }

            return JsonConvert.DeserializeObject<T>(str);
        }

        public void ClearDb()
        {
            _redis.GetServer("127.0.0.1", 6379).FlushAllDatabases(0);
        }
    }
}
