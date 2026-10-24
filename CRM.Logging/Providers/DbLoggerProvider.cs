using CRM.Logging.Entities;
using CRM.Logging.Model;
using CRM.Logging.Options;
using CRM.Logging.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Logging.Providers {
    /// <summary>
    /// A logger provider that writes log entries to a database.
    /// <para>"DbLog" is the provider alias of this provider and can be used in the Logging section of the appsettings.json.</para>
    /// </summary>
    [Microsoft.Extensions.Logging.ProviderAlias("DbLog")]
    public class DbLoggerProvider : LoggerProvider {
        /* private */
        bool Terminated;
        readonly IMongoLogRepository _repository;

        ConcurrentQueue<LogEntry> InfoQueue = new ConcurrentQueue<LogEntry>();

        /// <summary>
        /// Applies the log entry retains policy according to options
        /// </summary>
        void ApplyRetainPolicy() {
            try {
                int days = Settings.RetainPolicyRecordCountDays;
                long deletedDocCount = _repository.DeleteLogAsync(days);
            }
            catch {
            }
        }

        /// <summary>
        /// Pops a log info instance from the stack, prepares the log entry, and writes the log to the db table.
        /// </summary>
        void WriteLogLine() {
            LogEntry Info;
            if (InfoQueue.TryDequeue(out Info)) {

                var mongoLog = new DbLog();
                mongoLog.Category = Info.Category;
                mongoLog.EventLevel = Info.Level.ToString();
                mongoLog.EventTime = Info.TimeStampUtc != null ? Info.TimeStampUtc.ToLocalTime() : DateTime.Now;
                mongoLog.Text = Info.Text;
                mongoLog.ErrorText = Info.Exception?.ToString() ?? string.Empty;

                if (Info.StateProperties != null && Info.StateProperties.Count > 0) {
                    foreach (KeyValuePair<string, object> item in Info.StateProperties) {
                        if (item.Key.Equals("LogObject", StringComparison.CurrentCultureIgnoreCase)) {
                            if (item.Value is LogObject) {
                                var obj = item.Value as LogObject;
                                mongoLog.UserName = obj.UserName;
                                mongoLog.UserIpAddress = obj.UserIpAddress;
                                mongoLog.Ident = obj.Ident;
                                mongoLog.Controller = obj.Controller;
                                mongoLog.Action = obj.Action;
                                mongoLog.CommentText = obj.CommentText;
                            }
                        }
                        else if (item.Key.Equals("PayLoad", StringComparison.CurrentCultureIgnoreCase)) {
                            mongoLog.PayLoad = Newtonsoft.Json.JsonConvert.SerializeObject(item.Value);
                        }
                    }
                }

                _repository.AddLogAsync(mongoLog);
            }
        }

        void ThreadProc() {
            Task.Run(() => {

                while (!Terminated) {
                    try {
                        WriteLogLine();
                        System.Threading.Thread.Sleep(100);
                    }
                    catch // (Exception ex)
                    {
                    }
                }

            });
        }

        /* overrides */
        /// <summary>
        /// Disposes the options change toker. IDisposable pattern implementation.
        /// </summary>
        protected override void Dispose(bool disposing) {
            Terminated = true;
            base.Dispose(disposing);
        }

        /* construction */
        /// <summary>
        /// Constructor.
        /// <para>The IOptionsMonitor provides the OnChange() method which is called when the user alters the settings of this provider in the appsettings.json file.</para>
        /// </summary>
        public DbLoggerProvider(IOptionsMonitor<DbLoggerOptions> Settings, IMongoLogRepository repository)
            : this(Settings.CurrentValue, repository) {

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/change-tokens
            SettingsChangeToken = Settings.OnChange(settings => {
                this.Settings = settings;
            });
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DbLoggerProvider(DbLoggerOptions Settings, IMongoLogRepository repository) {
            this.Settings = Settings;
            this._repository = repository;

            // check the retain policy
            ApplyRetainPolicy();

            ThreadProc();
        }

        /* public */
        /// <summary>
        /// Checks if the given logLevel is enabled. It is called by the Logger.
        /// </summary>
        public override bool IsEnabled(LogLevel logLevel) {
            bool Result = logLevel != LogLevel.None
               && this.Settings.LogLevel != LogLevel.None
               && Convert.ToInt32(logLevel) >= Convert.ToInt32(this.Settings.LogLevel);

            return Result;
        }

        /// <summary>
        /// Writes the specified log information to a log db.
        /// </summary>
        public override void WriteLog(LogEntry Info) {
            InfoQueue.Enqueue(Info);
        }

        /* properties */
        /// <summary>
        /// Returns the settings
        /// </summary>
        internal DbLoggerOptions Settings { get; private set; }


    }
}
