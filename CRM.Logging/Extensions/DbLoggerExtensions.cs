using CRM.Logging.Options;
using CRM.Logging.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace CRM.Logging.Extensions {
    /// <summary>
    /// Db logger extension methods.
    /// </summary>
    static public class DbLoggerExtensions {
        /// <summary>
        /// Adds the db logger provider, aliased as 'DbLog', in the available services as singleton and binds the db logger options class to the 'DbLog' section of the appsettings.json file.
        /// </summary>
        static public ILoggingBuilder AddDbLogger(this ILoggingBuilder builder) {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DbLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<DbLoggerOptions>, DbLoggerOptionsSetup>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<DbLoggerOptions>, LoggerProviderOptionsChangeTokenSource<DbLoggerOptions, DbLoggerProvider>>());
            return builder;
        }
        /// <summary>
        /// Adds the db logger provider, aliased as 'DbLog', in the available services as singleton and binds the db logger options class to the 'DbLog' section of the appsettings.json file.
        /// </summary>
        static public ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerOptions> configure) {
            if (configure == null) {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddDbLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
