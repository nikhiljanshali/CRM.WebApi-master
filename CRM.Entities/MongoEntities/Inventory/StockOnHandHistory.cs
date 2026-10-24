using CRM.Core.MongoCore.Model;

namespace CRM.Entities.MongoEntities.Inventory {
    public class StockOnHandHistory : Document {
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
        public string Location { get; set; }
        public string Status { get; set; }
        public string Lab { get; set; }
        public string CertificateNo { get; set; }
        public bool IsHold { get; set; }
        public PriceDNorm BasePrice { get; set; }
        public PriceDNorm Price { get; set; }
        public IdentityDNorm FromIdentity { get; set; }
        public IdentityDNorm ToIdentity { get; set; }
        public StockOnHandHistory() {
            FromIdentity = new IdentityDNorm();
            ToIdentity = new IdentityDNorm();
            BasePrice = new PriceDNorm();
            Price = new PriceDNorm();
        }

    }
}