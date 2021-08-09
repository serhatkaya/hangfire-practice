using hangifre.practice.Helpers;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace hangifre.practice
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.File("Logs/log.txt")
               .WriteTo.Console()
               .CreateLogger();
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .UseStartup<Startup>()
                .UseSerilog();
        }
    }
}
