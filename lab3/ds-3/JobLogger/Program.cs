using Microsoft.Extensions.Configuration;
using NATS.Client;
using System;
using System.IO;

namespace JobLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string jsonPath = Directory.GetParent(currentDirectoryPath) + "/config/config.json";
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(jsonPath, optional: false)
                .Build();
                
            var subscriberService = new SubscriberService();

            using (IConnection connection = new ConnectionFactory().CreateConnection($"http://localhost:{config.GetValue<int>("NatsPort")}"))
            {
                subscriberService.Run(connection);
                Console.WriteLine("Monitoring starting:");
                Console.ReadKey();
            }
        }
    }
}
