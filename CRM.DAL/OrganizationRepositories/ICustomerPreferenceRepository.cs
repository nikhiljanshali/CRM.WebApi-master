using CRM.Entities.MongoEntities.Organization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public interface ICustomerPreferenceRepository {
        Task<CustomerPreference> GetCustomerPreferenceAsync(string customerId);
        Task<IList<CustomerPreference>> GetCustomerPreferencesAsync();

        Task<string> InsertCustomerPreferenceRepository(CustomerPreference customerPreference);

        Task<bool> UpdateCustomerPreferenceRepository(CustomerPreference customerPreference);

        Task<long> DeleteCustomerPreferenceRepository(string id);
    }
}
