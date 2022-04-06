using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SapConnectionTest.Configurations;
using SapConnectionTest.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SapConnectionTest
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ImAliveService _imAliveService;
        private readonly PingTest _pingTest;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ImAliveService imAliveService, PingTest pingTest)
        {
            _logger = logger;
            _configuration = configuration;
            _imAliveService = imAliveService;
            _pingTest = pingTest;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning("Worker started at: {date}", DateTimeOffset.Now);

            var applicationSettings = _configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {date}", DateTimeOffset.Now);

                try
                {
                    PingService.Ping(applicationSettings.SapAddress);

                    _pingTest.Ping();
                    await _imAliveService.Push();
                    _logger.LogInformation("Connect to SAP OK");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Connect to SAP KO: {ex}", ex.Message);
                }
                await Task.Delay(applicationSettings.DelayTime, stoppingToken);
            }
        }
    }
}
