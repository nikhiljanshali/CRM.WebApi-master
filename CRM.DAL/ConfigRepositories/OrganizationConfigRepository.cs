using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Configuration;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public class OrganizationConfigRepository : IOrganizationConfigRepository {
        private readonly IMongoRepository _repository;

        public OrganizationConfigRepository(IMongoRepository repository) {
            _repository = repository;
        }


        public async Task<OrganizationConfig> GetOrganizationConfigAsyncByOrganizationId(string id) {
            return await _repository.GetOneAsync<OrganizationConfig>(c => c.OrganizationDNorm.Id == id);
        }

        public async Task<string> AddOrganizationConfigAsync(OrganizationConfig orgConfig) {
            await _repository.AddOneAsync(orgConfig);

            return orgConfig.Id;
        }

        public async Task<bool> UpdateOrganizationConfigAsync(OrganizationConfig orgConfig) {
            var result = await _repository.UpdateOneAsync(orgConfig);

            return result;
        }

        public async Task<long> DeleteOrganizationConfigAsync(string id) {
            return await _repository.DeleteOneAsync<OrganizationConfig>(c => c.Id == id);
        }

    }
}
