using CRM.Entities.MongoEntities.Promotional;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.PromotionalRepositories {
    public interface ISchemeRepository {
        Task<Scheme> GetSchemeAsync(string id);
        Task<IList<Scheme>> GetSchemesAsync();
        Task<IList<Scheme>> GetSchemesAsync(bool isActive);

        Task<string> InsertSchemeAsync(Scheme scheme);

        Task<bool> UpdateSchemeAsync(Scheme scheme);

        Task<long> DeleteSchemeAsync(string id);

    }
}
