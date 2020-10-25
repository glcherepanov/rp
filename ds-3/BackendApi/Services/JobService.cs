using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using NATS.Client;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace BackendApi.Services
{
    public class JobService : Job.JobBase
    {
        private readonly ILogger<JobService> _logger;
        private IConfiguration _config;
        public JobService(ILogger<JobService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            string id = Guid.NewGuid().ToString();
            SaveToDb(id, request.Description);
            PublishEvent(id);
            var resp = new RegisterResponse
            {
                Id = id
            };

            return Task.FromResult(resp);
        }

        private void SaveToDb(string id, string description)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"localhost:{_config.GetValue<int>("RedisPort")}");
            IDatabase db = redis.GetDatabase();
            db.StringSet(id, description);
        }

        private void PublishEvent(string id)
        {
            var publisherService = new Publisher.PublisherService(_config);
            
            using (IConnection connection = new ConnectionFactory().CreateConnection($"localhost:{_config.GetValue<int>("NatsPort")}"))
            {
                publisherService.RunAsync(connection, id).Wait();
            }
        }
    }
}