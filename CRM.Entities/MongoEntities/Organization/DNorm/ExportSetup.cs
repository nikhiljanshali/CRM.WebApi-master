using CRM.Entities.MongoEntities.Configuration.Base;
using System;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class ExportSetup {
        public string API { get; set; }
        public string URL { get; set; }
        public string FTP { get; set; }
        public DateTime? FTPTime { get; set; }
        public string Email { get; set; }
        public DateTime? EmailTime { get; set; }
        public IList<ColumnConfig> Formats { get; set; }

        public ExportSetup() {
            Formats = new List<ColumnConfig>();
        }
    }
}