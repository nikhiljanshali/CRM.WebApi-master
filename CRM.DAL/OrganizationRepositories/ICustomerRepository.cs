using CRM.Business.Common;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public interface ICustomerRepository {
        Task<Customer> GetCustomerAsync(string id);
        Task<IList<Customer>> GetCustomersAsync();
        Task<IList<Customer>> GetCustomersAsync(string name);
        Task<IList<Customer>> GetCustomersAsync(int skip, int take);
        Task<string> InsertCustomerAsync(Customer customer);
        Task<bool> InsertAddressAsync(string customerId, Address address);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<long> DeleteCustomerAsync(string id);
        Task<bool> DeleteAddressAsync(string customerId, string addressId);
        Task<UserCredential> GetCustomerCredentialAsync(string Id);
    }
}
