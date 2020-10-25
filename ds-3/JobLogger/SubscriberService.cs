using Microsoft.Extensions.Configuration;
using NATS.Client;
using NATS.Client.Rx;
using NATS.Client.Rx.Ops;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JobLogger
{
    class SubscriberService
    {
        public void Run(IConnection connection)
        {
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string jsonPath = Directory.GetParent(currentDirectoryPath) + "/config/config.json";
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(jsonPath, optional: false)
                .Build();

            INATSObservable<string> events = connection.Observe(config.GetValue<string>("NatsBus"))
                    .Select(m => Encoding.Default.GetString(m.Data));

            events.Subscribe(id => Console.WriteLine(id + ", " + RedisDbContext.GetDesription(id)));
        }
    }
}
