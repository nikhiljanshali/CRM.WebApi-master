using CRM.Entities.MongoEntities.Inventory;
using System.Collections.Generic;

namespace CRM.Business.Inventory {
    public class StockOnHandResponse {

        public List<InventoryItems> Inventories;
        public long TotalCount;
        public StockOnHandResponse()
        {
            Inventories = new List<InventoryItems>();
        }
    }
}