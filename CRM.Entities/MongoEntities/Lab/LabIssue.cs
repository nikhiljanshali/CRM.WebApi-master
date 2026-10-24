using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Lab;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.LabCollection {
    public class LabIssue : Document {
        public LabDNorm Lab { get; set; }
        public LogisticDNorm Logistic { get; set; }
        public bool IsRecheck { get; set; }
        public List<string> RecheckReason { get; set; }
        public IList<LabIssueItem> LabIssueItems { get; set; }        
        public IdentityDNorm Identity { get; set; }      

        public LabIssue() {
            LabIssueItems = new List<LabIssueItem>();
            Lab = new LabDNorm();
            Logistic = new LogisticDNorm();
            Identity = new IdentityDNorm();
        }

    }
}
