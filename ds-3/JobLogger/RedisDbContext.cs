using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace JobLogger
{
    class RedisDbContext
    {
        public static string GetDesription(string id)
        {
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string jsonPath = Directory.GetParent(currentDirectoryPath) + "/config/config.json";
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(jsonPath, optional: false)
                .Build();
                
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"localhost:{config.GetValue<int>("RedisPort")}");
            IDatabase db = redis.GetDatabase();
            string description = db.StringGet(id);

            return description;
        }
    }
}