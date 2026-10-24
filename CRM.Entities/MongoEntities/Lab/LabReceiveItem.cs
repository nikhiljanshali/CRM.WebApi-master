using CRM.Entities.MongoEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Lab {
    public class LabReceiveItem {
        public string LabIssueID { get; set; }
        public string StoneId { get; set; }
        public bool isReceived { get; set; }
        public IdentityDNorm Identity { get; set; }

        public LabReceiveItem() { }
    }
}
