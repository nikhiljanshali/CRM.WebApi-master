using CRM.Logging.Model;
using System;

namespace CRM.Logging.Attributes {
    public class DbLoggerAttribute : Attribute {

        //Private fields
        private LogDataType logDataType;
        private string description;
        private string queryParam;

        /// <summary>
        /// Constructor that required two parameters: action and description.
        /// </summary>
        /// <param name="description">Description of Action</param>
        /// <param name="queryParam">Parameter for object inserting in Logging database</param>
        public DbLoggerAttribute(LogDataType logDataType, string queryParam = null, string description = null) {
            this.logDataType = logDataType;
            this.description = description;
            this.queryParam = queryParam;
        }

        public virtual LogDataType LogDataType {
            get { return logDataType; }
        }

        public virtual string QueryParam {
            get { return queryParam; }
        }

        public virtual string Description {
            get { return description; }
        }
    }
}
