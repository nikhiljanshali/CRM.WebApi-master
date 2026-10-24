using Microsoft.Extensions.Configuration;
using System.IO;

namespace CRM.Core.Common {
    public static class Configuration {
        public static IConfigurationRoot Config { get; set; }

        static Configuration() {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Config = builder.Build();

            Configuration.Config = Config;
        }

        public static string DbConnection => Config["Data:DefaultConnection:ConnectionString"];

        public static string DefaultPassword => Config["TokenAuthentication:DefaultPassword"];
    }
}