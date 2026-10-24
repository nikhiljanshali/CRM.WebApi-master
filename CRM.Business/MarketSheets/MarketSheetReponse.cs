using CRM.Entities.MongoEntities.Inventory;
using System.Collections.Generic;

namespace CRM.Business.MarketSheets {
    public class MarketSheetReponse {

        public List<InventoryItems> MarketSheets;
        public long TotalCount;
        public decimal? TotalWeight;
        public decimal? TotalNetAmount;
        public decimal? TotalBlockedStones;
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalAvgDPC { get; set; }


        public MarketSheetReponse() {
            TotalDiscount = 0;
            TotalAvgDPC = 0;
            MarketSheets = new List<InventoryItems>();
        }
    }
}