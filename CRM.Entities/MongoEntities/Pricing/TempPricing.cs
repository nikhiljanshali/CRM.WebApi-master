using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Inventory;

namespace CRM.Entities.MongoEntities.Pricing {
    public class TempPricing : Document {
        public string StoneId { get; set; }
        public string Kapan { get; set; }
        public string Article { get; set; }
        public string Grade { get; set; }
        public string Shape { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public string Lab { get; set; }
        public string Comments { get; set; }
        public string CertificateUrl { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public PriceDNorm BasePrice { get; set; }
        public PriceDNorm TempPrice { get; set; }
        public InventoryItemInclusion Inclusion { get; set; }
        public InventoryItemMeasurement Measurement { get; set; }
        public StoneOrgDNorm StoneOrg { get; set; }
        public IdentityDNorm Identity { get; set; }
        public decimal? DiscountOne { get; set; }
        public decimal? DiscountTwo { get; set; }
        public decimal? DiscountThree { get; set; }
        public bool? IsValid { get; set; }

        public TempPricing() {
            BasePrice = new PriceDNorm();
            TempPrice = new PriceDNorm();
            Inclusion = new InventoryItemInclusion();
            Measurement = new InventoryItemMeasurement();
            StoneOrg = new StoneOrgDNorm();
            Identity = new IdentityDNorm();
        }

    }

}