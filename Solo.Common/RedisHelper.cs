using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Solo.Common
{
    public class RedisHelper
    {
        //public readonly IDatabase _db;
        //public readonly ConnectionMultiplexer _redis;
        //public RedisHelper(string redisConnectStr)
        //{
        //    //_redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=zs19446613,allowAdmin=true");
        //    _redis = ConnectionMultiplexer.Connect(redisConnectStr);
        //    _db = _redis.GetDatabase();
        //}
        //public bool SetRedisData<T>(string key,T obj)
        //{
        //    string json = JsonConvert.SerializeObject(obj);//序列化
        //    return _db.StringSet(key, json);
           
        //}

        //public T  GetRedisData<T>(string key)
        //{
        //    try
        //    {
        //        string str = _db.StringGet(key);
        //        if (str == null)
        //        {
        //            str = "";
        //        }

        //        return JsonConvert.DeserializeObject<T>(str);
        //    }
        //    catch   
        //    {
        //        return default(T);

        //    }
            
        //}

        //public void ClearDb()
        //{
        //    _redis.GetServer("127.0.0.1", 6379).FlushAllDatabases(0);
        //}

        //public void Close()
        //{
        //    _redis.Close();
        //}
    }
}
