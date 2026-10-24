using CRM.Logging.Providers;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace CRM.Logging.Options {
    /// <summary>
    /// Configures a FileLoggerOptions instance by using ConfigurationBinder.Bind against an IConfiguration.
    /// <para>This class essentially binds a FileLoggerOptions instance with a section in the appsettings.json file.</para>
    /// </summary>
    internal class DbLoggerOptionsSetup : ConfigureFromConfigurationOptions<DbLoggerOptions> {
        /// <summary>
        /// Constructor that takes the IConfiguration instance to bind against.
        /// </summary>
        public DbLoggerOptionsSetup(ILoggerProviderConfiguration<DbLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration) {
        }
    }
}
