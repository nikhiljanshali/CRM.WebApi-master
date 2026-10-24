using CRM.Entities.MongoEntities.InSight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.InSightRepository {
    public interface IPreOrderStoneRepository {
        Task<PreOrderStone> GetPreOrderStoneAsync(string id);
        Task<IList<PreOrderStone>> GetPreOrderStonesbyCustomerAsync(string customerId);
        Task<IList<PreOrderStone>> GetPreOrderStonesAsync(bool isApproved, bool isActive);
        Task<IList<PreOrderStone>> GetPreOrderStonesPaginatedAsync(bool isApproved, bool isActive, int skip, int take);

        Task<string> InsertPreOrderStoneAsync(PreOrderStone preOrderStone);

        Task<bool> UpdatePreOrderStoneAsync(PreOrderStone preOrderStone);

        Task<long> DeletePreOrderStoneAsync(string id);
    }
}
