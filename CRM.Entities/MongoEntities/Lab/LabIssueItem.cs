using CRM.Entities.MongoEntities.Inventory;
using System;

namespace CRM.Entities.MongoEntities.Lab {
    public class LabIssueItem {
        public string StoneId { get; set; }
        public string Kapan { get; set; }
        public string Shape { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public PriceDNorm BasePrice { get; set; }
        public bool IsReceived { get; set; }
        public DateTime? ReceiveDate { get; set; }        

        public LabIssueItem() {
            BasePrice = new PriceDNorm();
        }

    }
}
