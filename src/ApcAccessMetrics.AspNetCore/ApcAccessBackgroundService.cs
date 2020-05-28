using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApcAccessMetrics.Common.DeSerializer;
using ApcAccessMetrcs.Common.Apc;

namespace ApcAccessMetrcs.AspNetCore
{
    public class ApcAccessBackgroundService: IHostedService, IDisposable
    {

        private readonly ILogger<ApcAccessBackgroundService> _logger;
        private Timer _timer;
        private ColonKeyValueDeSerializer _deserializer;

        public ApcAccessBackgroundService(ILogger<ApcAccessBackgroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ApcAccessBackgroundService)} running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            _deserializer = new ColonKeyValueDeSerializer();

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var text = @"Status : Online";
            var dict = _deserializer.Deserialize<ApcStatus>(text);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ApcAccessBackgroundService)} is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}