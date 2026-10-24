using CRM.Entities.MongoEntities.InSight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.InSightRepository {
    public interface IWatchListRepository {
        Task<WatchList> GetWatchListAsync(string id);
        Task<IList<WatchList>> GetWatchListsbyStoneIdAsync(string stoneId);
        Task<IList<WatchList>> GetWatchListsbyCustomerIdAsync(string customerId);
        Task<IList<WatchList>> GetPaginatedWatchLists(int skip, int take);

        Task<string> InsertWatchListAsync(WatchList watchList);

        Task<bool> UpdateWatchListAsync(WatchList watchList);

        Task<long> DeleteWatchListAsync(string id);

    }
}
