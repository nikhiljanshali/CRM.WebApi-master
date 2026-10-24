using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Inventory;

namespace CRM.Entities.MongoEntities.GradingMaster {
    public class GradingMaster : Document {
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
        public string StrLn { get; set; }
        public string LrHalf { get; set; }  
        public string Lab { get; set; }       
        public string Comments { get; set; }     
        public PriceDNorm BasePrice { get; set; }        
        public InventoryItemInclusion Inclusion { get; set; }
        public InventoryItemMeasurement Measurement { get; set; }
        public StoneOrgDNorm StoneOrg { get; set; }
        public IdentityDNorm Identity { get; set; }
        public string RapVer { get; set; }

        public GradingMaster() {
            BasePrice = new PriceDNorm();            
            Inclusion = new InventoryItemInclusion();
            Measurement = new InventoryItemMeasurement();
            StoneOrg = new StoneOrgDNorm();
            Identity = new IdentityDNorm();           
        }

    }
}