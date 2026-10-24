using CRM.Business.Common;
using CRM.Business.Labs;
using CRM.Entities.MongoEntities.GradingMaster;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Lab;
using CRM.Entities.MongoEntities.LabCollection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.LabRepositories {
    public interface ILabRepository {
        //GET
        Task<Lab> GetLabAsync(string id);
        Task<IList<Lab>> GetLabsAsync();
        Task<LabResponse> GetLabsAsync(LabSearchCriteria OrganizationCriteria, int skip, int take);
        //Insert
        Task<string> InsertLabAsync(Lab lab);
        //Update
        Task<bool> UpdateLabAsync(Lab lab);
        //Delete
        Task<long> DeleteLabAsync(string id);
        Task<CommonResponse> InsertLabIssue(LabIssue labIssue);
        Task<IList<LabIssue>> GetLabsIssueAsync();
        Task<bool> UpdateLabReceiveAsync(IList<LabReceiveItem> labReceiveItem);
        Task<IList<GradingMaster>> GetGradingFromStoneIdsAsync(IList<string> ids);
        Task<IList<InventoryItems>> GetInvItemsFromStoneIdsAsync(IList<string> ids);
        Task<bool> InsertLabResult(IList<LabResult> labResult);
        Task<CommonResponse> InsertLabRecheckIssueAsync(LabIssue labIssue);
        Task<bool> UpdateControllerNoFromStoneIds(IList<string> ids);
        Task<IList<string>> GetStoneIdsExistOrNotLabResultAsync(IList<string> ids);
    }
}