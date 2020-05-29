using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApcAccessMetrics.Common.Deserializer;
using ApcAccessMetrcs.Common.Apc;
using ApcAccessMetrcs.Common.Commands;
using System.Collections.Generic;
using System.Linq;
using ApcAccessMetrics.Common.Extensions;

namespace ApcAccessMetrcs.AspNetCore
{
    public class ApcAccessBackgroundService: IHostedService, IDisposable
    {

        private readonly ILogger<ApcAccessBackgroundService> _logger;
        private Timer _timer;
        private ColonKeyValueDeserializer _deserializer;
        private ApcAccessCommand _apcAccessCommand; 

        public ApcAccessBackgroundService(ILogger<ApcAccessBackgroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ApcAccessBackgroundService)} running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            _deserializer = new ColonKeyValueDeserializer();
            _apcAccessCommand = new ApcAccessCommand();

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var text = _apcAccessCommand.Run();
            var status = _deserializer.Deserialize<ApcStatus>(text);            
            var tags = status.GetMetricTags();
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