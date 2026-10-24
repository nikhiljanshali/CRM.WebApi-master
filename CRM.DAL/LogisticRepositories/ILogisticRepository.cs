using CRM.Business.Logistics;
using CRM.Entities.MongoEntities.LogisticCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.LogisticRepositories {
    public interface ILogisticRepository {
        //GET
        Task<Logistic> GetLogisticAsync(string id);
        Task<IList<Logistic>> GetLogisticsAsync();
        Task<LogisticResponse> GetLogisticsAsync(LogisticSearchCriteria logisticCriteria, int skip, int take);
        //Insert
        Task<string> InsertLogisticAsync(Logistic logistic);
        //Update
        Task<bool> UpdateLogisticAsync(Logistic logistic);
        //Delete
        Task<long> DeleteLogisticAsync(string id);
    }
}
