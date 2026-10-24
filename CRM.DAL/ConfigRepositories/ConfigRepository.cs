using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public class ConfigRepository : IConfigRepository {

        private readonly IMongoRepository _repository;

        public ConfigRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<GridConfig> GetGridConfigAsync(string empid, string pageName, string gridName) {
            return await _repository.GetOneAsync<GridConfig>(c => c.EmpID == empid && c.PageName == pageName && c.GridName== gridName);
        }

        public async Task<GridMasterConfig> GetMasterGridConfigAsync(string pageName, string gridName) {
            return await _repository.GetOneAsync<GridMasterConfig>(c => c.PageName == pageName && c.GridName == gridName);
        }

        public async Task<string> InsertGridConfigAsync(GridConfig gridConfig) {
            await _repository.AddOneAsync(gridConfig);

            return gridConfig.Id;
        }

        public async Task<string> InsertMasterGridConfigAsync(GridMasterConfig gridMasterConfig) {
            await _repository.AddOneAsync(gridMasterConfig);

            return gridMasterConfig.Id;
        }

        public async Task<bool> UpdateGridConfigAsync(GridConfig gridConfig) {
            var result = await _repository.UpdateOneAsync<GridConfig>(gridConfig);

            return result;
        }

        public async Task<bool> UpdateMasterGridConfigAsync(GridMasterConfig gridMasterConfig) {
            var result = await _repository.UpdateOneAsync<GridMasterConfig>(gridMasterConfig);

            return result;
        }
    }
}
