using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Common
{
    public class RedisConn
    {
        public readonly string readStr = AppConfigurtaionServices.Configuration.GetConnectionString("RedisSlave");
        public readonly string writeConn = AppConfigurtaionServices.Configuration.GetConnectionString("RedisMaster");
        public readonly IDatabase _db;
        public readonly ConnectionMultiplexer _redis;
        public RedisConn(bool isRead)
        {
            string[] readConns = readStr.Split(' '); 
            //_redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=zs19446613,allowAdmin=true");
            Random random = new Random();
            int n = random.Next(0, readConns.Length);
            if (isRead)
            {
                _redis = ConnectionMultiplexer.Connect(readConns[n]);
               
            }
            else
            {
                _redis = ConnectionMultiplexer.Connect(writeConn);
            }
            _db = _redis.GetDatabase();

        }
        public bool SetRedisData<T>(string key, T obj)
        {
            string json = JsonConvert.SerializeObject(obj);//序列化
            return _db.StringSet(key, json);

        }

        public T GetRedisData<T>(string key)
        {
            try
            {
                string str = _db.StringGet(key);
                if (str == null)
                {
                    str = "";
                }

                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {
                return default(T);

            }

        }

        public void ClearDb()
        {
            //_redis.GetServer("127.0.0.1", 6379).FlushAllDatabases(0);
        }

        public void Lpush(string FundNo,int UserId)
        {
            _db.ListLeftPushAsync("task", FundNo + "-" + UserId.ToString());
            //_redis.GetServer("127.0.0.1", 6379).FlushAllDatabases(0);
        }

        public void Close()
        {
            _redis.Close();
        }
    }
}
