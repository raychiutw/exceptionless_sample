using Exceptionless;
using Exceptionless.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sample.Exceptionless.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    // By default sends warning and error log messages to Exceptionless. Log levels can be controlled remotely per log source from the Exceptionless app in near real-time.
                    logging.AddExceptionless(c => c.SetDefaultMinLogLevel(LogLevel.Info));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}