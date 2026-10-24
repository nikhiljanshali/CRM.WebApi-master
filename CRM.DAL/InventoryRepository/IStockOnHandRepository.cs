using CRM.Business.Common;
using CRM.Business.Inventory;
using CRM.Entities.MongoEntities.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.StockOnHandRepositories {
    public interface IStockOnHandRepository {
        Task<IList<InventoryItems>> GetStockOnHandAsync();
        Task<StockOnHandResponse> GetStockOnHandAsync(StockOnHandSearchCriteria StockOnHandSearchCriteria, int skip, int take);
        Task<InventoryItems> GetStockOnHandAsync(string id);
        Task<IList<string>> UpdateStockAsync(IList<InventoryItems> inventoryItems);
        Task<long> UpdatePricingRequestAsync(IList<InventoryItems> inventoryItems);

        Task<InventoryItems> GetStockOnHandByStoneIdAsync(string stoneId);
        Task<CommonResponse> InsertConsignmentInvoice(ConsignmentInvoice consignmentInvoice);
        Task<IList<ConsignmentInvoice>> GetConsignmentInvoicesAsync();
        Task<long> UpdateInventoryMarketSheetAsync(IList<InventoryItems> inventoryItems);


    }
}
