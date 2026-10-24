using CRM.Business.Inventory;
using CRM.Entities.MongoEntities.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.InventoryRepositories {
    public interface IInventoryRepository {
        Task<InventoryItems> GetInventoryAsync(string id);
        Task<InventoryItems> GetInventoryByStoneIdAsync(string stoneId);
        Task<IList<InventoryItems>> GetInventoriesAsync();
        Task<IList<InventoryItems>> GetInventoriesWithOutRFIDAsync();
        Task<InventoryResponse> GetInventoriesAsync(InventorySearchCriteria InventoryCriteria, int skip, int take);
        Task<InventoryItemMeasurement> GetMeasurementAsync(string inventoryId);
        Task<InventoryItemInclusion> GetInclusionAsync(string inventoryId);
        
        Task<IList<string>> GetStoneIdsExistOrNotAsync(IList<string> ids);
        Task<string> InsertInventoryAsync(InventoryItems inventory);
        Task<IList<string>> UploadInventoryFileAsync(IList<InventoryExcelItems> inventoryExcelItems);
        Task<IList<string>> UploadInclusionFileAsync(IList<InclusionExcelItems> inclusionExcelItems);

        Task<bool> UpdateInventoryAsync(InventoryItems inventory);
        Task<bool> UpdateInventoryMeasurementAsync(string inventoryId, InventoryItemMeasurement measurement);
        Task<bool> UpdateInventoryInclusionAsync(string inventoryId, InventoryItemInclusion inclusion);
        Task<long> UpdateInventoryHoldAsync(IList<InventoryItems> inventory);
        Task<bool> UpdateInventoryListAsync(IList<InventoryItems> inventory);
        Task<IList<string>> UpdateInventoryItemsAsync(IList<InventoryItems> inventoryItems);

        Task<long> DeleteInventoryAsync(string id);
        Task<bool> DeleteInventoryMeasurementAsync(string inventoryId);
        Task<bool> DeleteInventoryInclusionAsync(string inventoryId);
    }
}
