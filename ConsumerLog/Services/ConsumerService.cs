using ConsumerLog.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsumerLog.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly IConfiguration _configuration;
        private IMethodReceiveService _messageReceiver;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ConsumerService> _logger;

        public ConsumerService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration,IMethodReceiveService messageReceiver,ILogger<ConsumerService> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            _messageReceiver = messageReceiver;
            
            
        }


        public void StartConsumeData(string topic)
        {
                    Task.Run(() => _messageReceiver.Consume(
                                topic,
                                HandleConsumer,
                                HandleException));
        }

        public void HandleException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        private void HandleConsumer(string data)
        {
            //Console.WriteLine($"Data: {data}");
            _logger.LogInformation($"Data : {data}");

        }
    }
}
