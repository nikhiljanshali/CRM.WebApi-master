using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Promotional;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.PromotionalRepositories {
    public class SchemeRepository : ISchemeRepository {
        private readonly IMongoRepository _repository;

        public SchemeRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<Scheme> GetSchemeAsync(string id) {
            return await _repository.GetOneAsync<Scheme>(c => c.Id == id);
        }

        public async Task<IList<Scheme>> GetSchemesAsync() {
            return await _repository.GetAllAsync<Scheme>(c => c.Id != null);
        }

        public async Task<IList<Scheme>> GetSchemesAsync(bool isActive) {
            return await _repository.GetAllAsync<Scheme>(c => c.IsActive == isActive);
        }

        public async Task<string> InsertSchemeAsync(Scheme scheme) {
            await _repository.AddOneAsync(scheme);

            return scheme.Id;
        }

        public async Task<bool> UpdateSchemeAsync(Scheme scheme) {
            return await _repository.UpdateOneAsync(scheme);
        }

        public async Task<long> DeleteSchemeAsync(string id) {
            return await _repository.DeleteOneAsync<Scheme>(c => c.Id == id);
        }

    }
}
