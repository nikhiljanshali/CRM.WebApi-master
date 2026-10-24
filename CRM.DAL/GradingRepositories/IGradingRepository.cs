using CRM.Entities.MongoEntities.GradingMaster;
using CRM.Entities.MongoEntities.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.GradingRepositories {
    public interface IGradingRepository {
        Task<IList<GradingMaster>> GetGradingAsync(string StoneId);
        Task<IList<string>> InsertGradingAsync(IList<InventoryItems> inventoryItems, string RepVer);
        Task<InventoryItems> UpdateGradingAsync(GradingMaster GradingMaster);
    }
}
