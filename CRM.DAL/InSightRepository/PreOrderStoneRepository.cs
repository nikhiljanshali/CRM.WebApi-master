using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.InSight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.InSightRepository {
    public class PreOrderStoneRepository : IPreOrderStoneRepository {
        private readonly IMongoRepository _repository;

        public PreOrderStoneRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<PreOrderStone> GetPreOrderStoneAsync(string id) {
            return await _repository.GetOneAsync<PreOrderStone>(c => c.Id == id);
        }

        public async Task<IList<PreOrderStone>> GetPreOrderStonesbyCustomerAsync(string customerId) {
            return await _repository.GetAllAsync<PreOrderStone>(c => c.Customer.Id == customerId);
        }

        public async Task<IList<PreOrderStone>> GetPreOrderStonesAsync(bool isApproved, bool isActive) {
            return await _repository.GetAllAsync<PreOrderStone>(c => c.IsApproved == isApproved && c.IsActive == isActive);
        }        

        public async Task<IList<PreOrderStone>> GetPreOrderStonesPaginatedAsync(bool isApproved, bool isActive, int skip, int take) {
            return await _repository.GetPaginatedAsync<PreOrderStone>(c => c.IsApproved == isApproved && c.IsActive == isActive, skip, take);
        }

        public async Task<string> InsertPreOrderStoneAsync(PreOrderStone preOrderStone) {
            await _repository.AddOneAsync(preOrderStone);

            return preOrderStone.Id;
        }

        public async Task<bool> UpdatePreOrderStoneAsync(PreOrderStone preOrderStone) {
            return await _repository.UpdateOneAsync(preOrderStone);
        }

        public async Task<long> DeletePreOrderStoneAsync(string id) {
            return await _repository.DeleteOneAsync<PreOrderStone>(c => c.Id == id);
        }
    }
}
