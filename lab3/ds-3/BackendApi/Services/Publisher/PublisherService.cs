using NATS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BackendApi.Services.Publisher
{
    public class PublisherService
    {
        private IConfiguration _config;

        public PublisherService()
        {
        }

        public PublisherService(IConfiguration config)
        {
            _config = config;
        }
        public async Task RunAsync(IConnection connection, string id)
        {
            byte[] payload = Encoding.Default.GetBytes(id);
            connection.Publish(_config.GetValue<string>("NatsBus"), payload);

            await Task.Delay(1000);
        }
    }
}