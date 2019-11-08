using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumerLog.Interfaces;

namespace ConsumerLog.Services
{
    public class MethodReceiveService : IMethodReceiveService
    {
        private readonly ConsumerConfig _config;
        private readonly IConfiguration _configuration;

        public MethodReceiveService(IConfiguration configuration)
        {
            _configuration = configuration;
            _config = new ConsumerConfig
            {
                BootstrapServers = _configuration.GetValue<string>("Kafka:Consumer:BootstrapServers"),
                GroupId = $"{Guid.NewGuid()}",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public void Consume(string topic, Action<string> processingMethod, Action<Exception> errorHandlingMethod)
        {
            using (var consumer = new ConsumerBuilder<string, string>(_config).Build())
            {

                consumer.Subscribe(topic);

                try
                {
                    var result = consumer.Consume();
                    var message = result.Message.Value;

                    processingMethod(message);
                }
                catch (ConsumeException e)
                {
                    errorHandlingMethod(e);
                }

                consumer.Close();
            }
        }
    }
}
