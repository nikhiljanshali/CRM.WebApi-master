using CRM.Business.Pricing;
using CRM.Entities.MongoEntities.Pricing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.PricingRepositories {
    public interface IRapPriceRepository {
        Task<List<RapPrice>> GetAllRapPriceAsync();
        Task<RapPrice> GetRapPriceAsync(RapPriceRequest req);
        Task<bool> UpdateRapPrice(IList<RapPrice> data);
    }
}
