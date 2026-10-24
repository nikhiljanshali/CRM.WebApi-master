using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.InSight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.InSightRepository {
    public class WatchListRepository : IWatchListRepository {
        private readonly IMongoRepository _repository;

        public WatchListRepository(IMongoRepository repository) {
            _repository = repository;
        }

        
        public async Task<WatchList> GetWatchListAsync(string id) {
            return await _repository.GetOneAsync<WatchList>(c => c.Id == id);
        }

        public async Task<IList<WatchList>> GetWatchListsbyCustomerIdAsync(string customerId) {
            return await _repository.GetAllAsync<WatchList>(c => c.Customer.Id == customerId);
        }

        public async Task<IList<WatchList>> GetWatchListsbyStoneIdAsync(string stoneId) {
            return await _repository.GetAllAsync<WatchList>(c => c.StoneId == stoneId);
        }

        public async Task<IList<WatchList>> GetPaginatedWatchLists(int skip, int take) {
            return await _repository.GetPaginatedAsync<WatchList>(c => c.Id != null, skip, take);
        }

        public async Task<string> InsertWatchListAsync(WatchList watchList) {
            await _repository.AddOneAsync(watchList);

            return watchList.Id;
        }

        public async Task<bool> UpdateWatchListAsync(WatchList watchList) {
            return await _repository.UpdateOneAsync(watchList);
        }

        public async Task<long> DeleteWatchListAsync(string id) {
            return await _repository.DeleteOneAsync<WatchList>(c => c.Id == id);
        }

    }
}
