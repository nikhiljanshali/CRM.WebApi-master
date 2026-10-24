using CRM.Business.Pricing;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Pricing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.PricingRepositories {
    public interface IPriceRequestRepository {
        Task<IList<TempPricing>> GetTempPricingDataAsync(string empId);
        Task<PriceReqResponse> GetPriceRequestDataAsync(PriceRequestApiModel req);
        Task<List<PricingHistory>> GetPricingHistoryByStoneAsync(string stoneId);
        Task<bool> InsertTempPrices(IList<TempPricing> pricing);
        Task<string> InsertTempPrice(TempPricing pricing);
        Task<bool> InsertPricingRequestFromInvItems(IList<InventoryItems> inv, string status);
        Task<bool> InsertPricingRequests(IList<PricingRequest> pricing);
        Task<string> InsertPricingRequest(PricingRequest pricing);
        Task<long> RemoveTempPrice(IList<TempPricing> pricing);
        Task<long> RemovePricingRequest(IList<PricingRequest> pricing);
    }
}
