using CRM.Entities.MongoEntities.Inventory;
using System.Collections.Generic;

namespace CRM.Business.MarketSheets {
    public class MarketSheetSellerResponse {

        public List<InventoryItems> MarketSheets;

        public long TotalCount;

        public decimal? TotalWeight;

        public decimal? TotalNetAmount;
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalAvgDPC { get; set; }
        public decimal? TotalHolds { get; set; }
        public decimal? TotalRapNetHolds { get; set; }
        public decimal? TotalMemos { get; set; }
        public decimal? TotalLeads { get; set; }
        public decimal? TotalAvailable { get; set; }
        public decimal? TotalHK { get; set; }
        public decimal? TotalIndia { get; set; }
        public decimal? TotalBelgium { get; set; }


        public MarketSheetSellerResponse() {
            this.TotalDiscount = 0;
            this.TotalAvgDPC = 0;
            this.MarketSheets = new List<InventoryItems>();
        }
    }
}
