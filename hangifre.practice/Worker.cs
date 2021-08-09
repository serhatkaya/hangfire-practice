using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hangifre.practice
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var manager = new RecurringJobManager();
            manager.AddOrUpdate("some-id", Job.FromExpression(() => SayMyName("serhatkaya")), Cron.Minutely());
        }
        public void SayMyName(string userName)
        {
            //Logic to Mail the user
            _logger.LogInformation($"Recurring job, {userName} {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
        }
    }
}
