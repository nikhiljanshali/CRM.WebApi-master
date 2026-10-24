using CRM.Entities.MongoEntities.InSight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.InSightRepository {
    public interface IOfferedStoneRepository {
        Task<OfferedStone> GetOfferedStoneAsync(string id);
        Task<IList<OfferedStone>> GetOfferedStonesbyStoneIdAsync(string stoneId);
        Task<IList<OfferedStone>> GetOfferedStonesbyCustomerIdAsync(string customerId);
        Task<IList<OfferedStone>> GetOfferedStonesPaginatedAsync(int skip, int take);

        Task<string> InsertOfferedStoneAsync(OfferedStone offeredStone);

        Task<bool> UpdateOfferedStoneAsync(OfferedStone offeredStone);

        Task<long> DeleteOfferdStoneAsync(string id);
    }
}
