using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.InSight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.InSightRepository {
    public class OfferedStoneRepository : IOfferedStoneRepository {
        private IMongoRepository _repository;

        public OfferedStoneRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<OfferedStone> GetOfferedStoneAsync(string id) {
            return await _repository.GetOneAsync<OfferedStone>(c => c.Id == id);
        }

        public async Task<IList<OfferedStone>> GetOfferedStonesbyCustomerIdAsync(string customerId) {
            return await _repository.GetAllAsync<OfferedStone>(c => c.Customer.Id == customerId && c.IsActive == true);
        }

        public async Task<IList<OfferedStone>> GetOfferedStonesbyStoneIdAsync(string stoneId) {
            return await _repository.GetAllAsync<OfferedStone>(c => c.StoneId == stoneId && c.IsActive == true);
        }

        public async Task<IList<OfferedStone>> GetOfferedStonesPaginatedAsync(int skip, int take) {
            return await _repository.GetPaginatedAsync<OfferedStone>(c => c.Id != null, skip, take);
        }

        public async Task<string> InsertOfferedStoneAsync(OfferedStone offeredStone) {
            await _repository.AddOneAsync(offeredStone);

            return offeredStone.Id;
        }

        public async Task<bool> UpdateOfferedStoneAsync(OfferedStone offeredStone) {
            return await _repository.UpdateOneAsync(offeredStone);
        }

        public Task<long> DeleteOfferdStoneAsync(string id) {
            return _repository.DeleteOneAsync<OfferedStone>(c => c.Id == id);
        }
    }
}
