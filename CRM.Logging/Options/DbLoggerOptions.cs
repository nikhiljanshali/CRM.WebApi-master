using Microsoft.Extensions.Logging;

namespace CRM.Logging.Options {
    /// <summary>
    /// Options for the Db logger.
    /// <para>There are two ways to configure db logger: 1. using the ConfigureLogging() in Program.cs or using the appsettings.json file.</para>
    /// <para> 1. ConfigureLogging()</para>
    /// <code>
    /// .ConfigureLogging(logging =&gt;
    /// {
    ///     logging.ClearProviders();
    ///     // logging.AddFileLogger(); 
    ///     logging.AddFileLogger(options =&gt; {  
    ///         options.MaxFileSizeInMB = 5;
    ///     });
    /// }) 
    /// </code>
    /// <para> 2. appsettings.json file </para>
    /// <code>
    ///   "Logging": {
    ///     "LogLevel": {
    ///       "Default": "Warning"
    ///     },
    ///     "DbLog": {
    ///       "LogLevel": "Debug",
    ///       "RetainPolicyRecordCount": 3,
    ///       "ConnectionString" : "Data Source=*********;initial catalog=****;persist security info=True;user id=**;password=****;"
    ///     }
    ///   },
    /// </code>
    /// </summary>

    public class DbLoggerOptions {
        private string connectionString;
        int retainPolicyRecordCountDays;

        /* construction */
        /// <summary>
        /// Constructor
        /// </summary>
        public DbLoggerOptions() {
        }

        /* properties */
        /// <summary>
        /// The active log level. Defaults to LogLevel.Information
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// The ConnectionString where log entry should be stored.
        /// </summary>
        public string ConnectionString {
            get { return connectionString; }
            set { connectionString = value; }
        }

        /// <summary>
        /// The maximum number of log files to retain. Defaults to 5.
        /// </summary>
        public int RetainPolicyRecordCountDays {
            get { return retainPolicyRecordCountDays < 1 ? 1 : retainPolicyRecordCountDays; }
            set { retainPolicyRecordCountDays = value; }
        }
    }
}
