using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Organization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public class CustomerPreferenceRepository : ICustomerPreferenceRepository {
        private readonly IMongoRepository _repository;

        public CustomerPreferenceRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<CustomerPreference> GetCustomerPreferenceAsync(string customerId) {
            return await _repository.GetOneAsync<CustomerPreference>(c => c.Customer.Id == customerId);
        }

        public async Task<IList<CustomerPreference>> GetCustomerPreferencesAsync() {
            return await _repository.GetAllAsync<CustomerPreference>(c => c.Id != null);
        }

        public async Task<string> InsertCustomerPreferenceRepository(CustomerPreference customerPreference) {
            await _repository.AddOneAsync(customerPreference);

            return customerPreference.Id;
        }

        public async Task<bool> UpdateCustomerPreferenceRepository(CustomerPreference customerPreference) {
            return await _repository.UpdateOneAsync<CustomerPreference>(customerPreference);
        }

        public async Task<long> DeleteCustomerPreferenceRepository(string id) {
            return await _repository.DeleteOneAsync<CustomerPreference>(c => c.Id == id);
        }

    }
}
