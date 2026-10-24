using CRM.Business.Common;
using CRM.Business.MarketSheets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.MarketSheetRepositories {
    public interface IMarketSheetRepository {

        Task<bool> SetBlockClients(string InventoryId, IList<string> BlockClientIds);
        Task<MarketSheetReponse> GetMarketSheetsAsync(MarketSheetSearchCriteria MarketSheetCriteria, int skip, int take);
    }
}
