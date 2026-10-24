using CRM.Entities.MongoEntities.Configuration;
using CRM.Entities.MongoEntities.Configuration.DNorm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public interface IOrganizationConfigRepository {
        Task<OrganizationConfig> GetOrganizationConfigAsyncByOrganizationId(string id);
        Task<string> AddOrganizationConfigAsync(OrganizationConfig orgConfig);
        Task<bool> UpdateOrganizationConfigAsync(OrganizationConfig orgConfig);
        Task<long> DeleteOrganizationConfigAsync(string id);

    }
}
