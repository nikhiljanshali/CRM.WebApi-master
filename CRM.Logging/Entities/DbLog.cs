using CRM.Core.MongoCore.Model;
using System;

namespace CRM.Logging.Entities {
    public class DbLog : Document{

        /* properties */
        /// <summary>
        /// User Name which who is logged in system.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User IP Address currently calling action.
        /// </summary>
        public string UserIpAddress { get; set; }

        /// <summary>
        /// Controller which is currently called.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action which is currently performed. 
        /// <para>We must writer in formated manner so it will help us to find log entry.</para>
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// User Comment. 
        /// <para>User Can Easily Find Log With User Comment.</para>
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Any Other Id For which we did not do any provision.
        /// </summary>
        public string Ident { get; set; }

        public string Category { get; set; }

        public DateTime EventTime { get; set; }
        public string EventLevel { get; set; }

        public string Text { get; set; }
        public string ErrorText { get; set; }

        public string PayLoad { get; set; }

        public DbLog() { }
    }
}
