using CRM.Entities.MongoEntities.Inventory;
using System.Collections.Generic;

namespace CRM.Business.Inventory {
    public class InventoryResponse {

        public List<InventoryItems> Inventories;
        public long TotalCount;
        public decimal? TotalWeight;
        public decimal? TotalNetAmount;
        public InventoryResponse()
        {
            Inventories = new List<InventoryItems>();
        }
    }
}