using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace hangifre.practice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHostedService<Worker>()
                .AddHangfire(configuration => configuration
                         .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                         .UseSimpleAssemblyNameTypeSerializer()
                         .UseRecommendedSerializerSettings()
                         .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                         {
                             CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                             SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                             QueuePollInterval = TimeSpan.Zero,
                             UseRecommendedIsolationLevel = true,
                             DisableGlobalLocks = true
                         }))
                    .AddHangfireServer();
        }
    }
}
