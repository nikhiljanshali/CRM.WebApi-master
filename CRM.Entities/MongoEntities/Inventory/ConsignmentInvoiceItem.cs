using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Inventory {
    public class ConsignmentInvoiceItem {
        public string StoneId { get; set; }
        public string Kapan { get; set; }
        public string Shape { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescent { get; set; }
        public PriceDNorm BasePrice { get; set; }
        public bool IsReceived { get; set; }
        public DateTime? ReceiveDate { get; set; }

        public ConsignmentInvoiceItem() {
            this.BasePrice = new PriceDNorm();
        }
    }
}
