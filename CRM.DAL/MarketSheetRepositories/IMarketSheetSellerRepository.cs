using CRM.Business.MarketSheets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.MarketSheetRepositories {
    public interface IMarketSheetSellerRepository {
        Task<MarketSheetSellerResponse> GetMarketSheetsAsync(MarketSheetSellerSearchCriteria MarketSheetSellerCriteria, int skip, int take);
    }
}
