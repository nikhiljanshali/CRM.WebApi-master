using CRM.Business.Common;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public class CustomerRepository : ICustomerRepository {
        private readonly IMongoRepository _repository;

        public CustomerRepository(IMongoRepository repository) {
          _repository = repository;
        }

        public async Task<Customer> GetCustomerAsync(string id) {
            return await _repository.GetOneAsync<Customer>(c => c.Id == id);
        }

        public async Task<IList<Customer>> GetCustomersAsync() {
            return await _repository.GetAllAsync<Customer>(c => c.Id != null);
        }

        public async Task<IList<Customer>> GetCustomersAsync(string name) {
            return await _repository.GetAllAsync<Customer>(c => c.FullName.Contains(name));
        }

        public async Task<IList<Customer>> GetCustomersAsync(int skip, int take) {
            return await _repository.GetPaginatedAsync<Customer>(c => c.Id != null, skip, take);
        }

        public async Task<string> InsertCustomerAsync(Customer customer) {
            await _repository.AddOneAsync(customer);

            return customer.Id;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer) {
            var result = await _repository.UpdateOneAsync(customer);

            return result;
        }

        public async Task<long> DeleteCustomerAsync(string id) {
            return await _repository.DeleteOneAsync<Customer>(c => c.Id == id);
        }

        public async Task<UserCredential> GetCustomerCredentialAsync(string id) {
            var result = await _repository.ProjectOneAsync<Customer, UserCredential>(c => c.Id == id,
                j => new UserCredential {
                    Id = j.Id,
                    FullName = j.FullName,
                    Organization = j.Organization.Name,
                    OrganizationId = j.Organization.Id,
                    Department = null,
                    DepartmentId = null,
                    Branch = null,
                    Code = null,
                    Origin = j.Origin,
                    IsAvailable = true,
                    IsManager = true
                });

            return result;
        }

        public Task<bool> InsertAddressAsync(string customerId, Address address) {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAddressAsync(string customerId, string addressId) {
            throw new System.NotImplementedException();
        }
    }
}
