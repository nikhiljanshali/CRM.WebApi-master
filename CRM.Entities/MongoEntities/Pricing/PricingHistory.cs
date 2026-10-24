using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Organization.DNorm;

namespace CRM.Entities.MongoEntities.Pricing {
    public class PricingHistory : Document {
        public string StoneId { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public decimal Weight { get; set; }
        public OrganizationDNorm Organization { get; set; }
        public PriceDNorm Price { get; set; }

        public PricingHistory() {
            Organization = new OrganizationDNorm();
            Price = new PriceDNorm();
        }
    }
}
