using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CRM.Logging.Extensions;

namespace CRM.Pricing {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder => {
                     webBuilder.CaptureStartupErrors(true);
                     webBuilder.ConfigureLogging((context, logging) => {
                         logging.ClearProviders();
                         logging.AddDbLogger();
                     });
                     webBuilder.UseSetting("detailedErrors", "true");
                     webBuilder.UseStartup<Startup>();
                 });
    }
}
