using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Lab;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Inventory {
    public class ConsignmentInvoice : Document {
        public OrganizationDNorm Organization { get; set; }
        public LogisticDNorm Logistic { get; set; }
        public IList<ConsignmentInvoiceItem> ConsignmentInvoiceItems { get; set; }
        public IdentityDNorm Identity { get; set; }

        public ConsignmentInvoice() {
            this.ConsignmentInvoiceItems = new List<ConsignmentInvoiceItem>();
            this.Organization = new OrganizationDNorm();
            this.Logistic = new LogisticDNorm();
            this.Identity = new IdentityDNorm();
        }

    }
}
