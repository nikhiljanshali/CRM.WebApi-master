using CRM.Core.MongoCore.Model;
using System;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.Inventory {
    public class InventoryItems : Document {
        public string StoneId { get; set; }
        public string Kapan { get; set; }
        public string Article { get; set; }
        public string RFID { get; set; }
        public string Grade { get; set; }
        public string Shape { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public string StrLn { get; set; }
        public string LrHalf { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public bool IsCPBlocked { get; set; }
        public IList<string> BlockedClients { get; set; }
        public string Lab { get; set; }
        public string CertificateNo { get; set; }
        public string Inscription { get; set; }
        public string CertificateUrl { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Comments { get; set; }
        public bool IsHold { get; set; }
        public bool IsRapnetHold { get; set; }
        public bool IsPricingRequest { get; set; }
        public bool IsMemo { get; set; }
        public PriceDNorm BasePrice { get; set; }
        public PriceDNorm Price { get; set; }
        public InventoryItemInclusion Inclusion { get; set; }
        public InventoryItemMeasurement Measurement { get; set; }
        public StoneOrgDNorm StoneOrg { get; set; }
        public IdentityDNorm Identity { get; set; }
        public DateTime? LabSendDate { get; set; }
        public DateTime? LabReceiveDate { get; set; }
        public DateTime? MarketSheetDate { get; set; }

        public InventoryItems() {
            BasePrice = new PriceDNorm();
            Price = new PriceDNorm();
            Inclusion = new InventoryItemInclusion();
            Measurement = new InventoryItemMeasurement();
            StoneOrg = new StoneOrgDNorm();
            Identity = new IdentityDNorm();
            BlockedClients = new List<string>();
            IsHold = false;
            IsRapnetHold = false;
            IsPricingRequest = false;
            IsMemo = false;
        }

    }
}