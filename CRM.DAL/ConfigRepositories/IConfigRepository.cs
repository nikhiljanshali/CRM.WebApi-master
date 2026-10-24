using CRM.Entities.MongoEntities.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public interface IConfigRepository {
        Task<GridMasterConfig> GetMasterGridConfigAsync(string pageName, string gridName);
        Task<string> InsertMasterGridConfigAsync(GridMasterConfig gridMasterConfig);
        Task<bool> UpdateMasterGridConfigAsync(GridMasterConfig gridMasterConfig);
        Task<GridConfig> GetGridConfigAsync(string id, string pageName, string gridName);
        Task<string> InsertGridConfigAsync(GridConfig gridConfig);
        Task<bool> UpdateGridConfigAsync(GridConfig gridConfig);
    }
}
