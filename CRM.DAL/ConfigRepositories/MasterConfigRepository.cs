using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Configuration;
using CRM.Entities.MongoEntities.Configuration.DNorm;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public class MasterConfigRepository : IMasterConfigRepository {
        private readonly IMongoRepository _repository;

        public MasterConfigRepository(IMongoRepository repository) {
            _repository = repository;
        }

        #region Get

        public async Task<MasterConfig> GetMasterConfigAsync() {
            return await _repository.GetOneAsync<MasterConfig>(c => c.Id != null);
        }

        public async Task<IList<MasterDNorm>> GetAllInclusionAsync() {
            var inclusion = await _repository.ProjectOneAsync<MasterConfig, IList<MasterDNorm>>(c => c.Id != null, j => j.Inclusions);

            return inclusion;
        }

        public async Task<IList<MasterDNorm>> GetAllMeasurementAsync() {
            var measurement = await _repository.ProjectOneAsync<MasterConfig, IList<MasterDNorm>>(c => c.Id != null, j => j.Measurements);

            return measurement;
        }


        #endregion

        #region Insert

        public async Task<bool> AddClaritiesAsync(MasterDNorm clarities) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            clarities.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Clarities, clarities);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddColorsAsync(MasterDNorm colors) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            colors.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Colors, colors);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddCPSAsync(MasterDNorm cps) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            cps.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.CPS, cps);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddCutAsync(MasterDNorm cut) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            cut.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Cut, cut);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddCutDetailsAsync(List<CutDetailDNorm> cutDetails) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            cutDetails.ForEach(c => { c.Id = ObjectId.GenerateNewId().ToString(); });
            var update = Builders<MasterConfig>.Update.PushEach(c => c.CutDetails, cutDetails);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddFluorescenceAsync(MasterDNorm fluorescence) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            fluorescence.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Fluorescence, fluorescence);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddLabAsync(MasterDNorm lab) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            lab.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Lab, lab);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddShapeAsync(MasterDNorm shape) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            shape.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Shape, shape);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<string> AddMasterConfigAsync(MasterConfig masterConfig) {
            await _repository.AddOneAsync(masterConfig);

            return masterConfig.Id;
        }

        public async Task<bool> AddInclusionAsync(MasterDNorm inclusion) {
            bool flag = false;

            var masterConfig = await GetMasterConfigAsync();
            inclusion.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Inclusions, inclusion);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }

        public async Task<bool> AddMeasurementAsync(MasterDNorm measurement) {
            bool flag = false;

            var masterConfig = await GetMasterConfigAsync();
            measurement.Id = ObjectId.GenerateNewId().ToString();
            var update = Builders<MasterConfig>.Update.Push(c => c.Measurements, measurement);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);

            return flag;
        }


        #endregion

        #region Delete

        public async Task<bool> DeleteClaritiesAsync(string claritiesId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Clarities, f => f.Id == claritiesId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteColorsAsync(string colorsId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Colors, f => f.Id == colorsId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteCPSAsync(string cpsId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.CPS, f => f.Id == cpsId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteCutAsync(string cutId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Cut, f => f.Id == cutId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteCutDetailsAsync(string cutDetailsId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.CutDetails, f => f.Id == cutDetailsId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteFluorescenceAsync(string fluorescenceId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Fluorescence, f => f.Id == fluorescenceId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteLabAsync(string labId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Lab, f => f.Id == labId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteShapeAsync(string shapeId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Shape, f => f.Id == shapeId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteInclusionAsync(string inclusionId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Inclusions, f => f.Id == inclusionId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }

        public async Task<bool> DeleteMeasurementAsync(string measurementId) {
            bool flag = false;
            var masterConfig = await GetMasterConfigAsync();

            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Measurements, f => f.Id == measurementId);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);

            return flag;
        }


        #endregion

        #region Update

        public async Task<bool> UpdateClaritiesAsync(MasterDNorm clarities) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Clarities, f => f.Id == clarities.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Clarities, clarities);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateColorsAsync(MasterDNorm colors) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Colors, f => f.Id == colors.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Colors, colors);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateCPSAsync(MasterDNorm cps) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.CPS, f => f.Id == cps.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.CPS, cps);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateCutAsync(MasterDNorm cut) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Cut, f => f.Id == cut.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Cut, cut);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateFluorescenceAsync(MasterDNorm fluorescence) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Fluorescence, f => f.Id == fluorescence.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Fluorescence, fluorescence);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateLabAsync(MasterDNorm lab) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Lab, f => f.Id == lab.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Lab, lab);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateShapeAsync(MasterDNorm shape) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Shape, f => f.Id == shape.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Shape, shape);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateInclusionAsync(MasterDNorm inclusion) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Inclusions, f => f.Id == inclusion.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Inclusions, inclusion);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateInclusionConfigAsync(MasterConfigType inclusionType) {
            var flag = false;
            var masterConfig = await GetMasterConfigAsync();
            var update = Builders<MasterConfig>.Update.Set("InclusionConfig."+ inclusionType.Type, inclusionType.IsActive);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            if (flag && !inclusionType.IsActive) {
                var count = masterConfig.Inclusions.Where(f => f.Type == inclusionType.Type.ToLower()).Count();
                if (count > 0) {
                    var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Inclusions, f => f.Type == inclusionType.Type.ToLower());
                    flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
                }

            }

            return flag;
        }

        public async Task<bool> UpdateMeasurementAsync(MasterDNorm measurement) {
            var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Measurements, f => f.Id == measurement.Id);
            var masterConfig = await GetMasterConfigAsync();

            var flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
            if (flag) {
                var update = Builders<MasterConfig>.Update.Push(c => c.Measurements, measurement);
                flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            }

            return flag;
        }

        public async Task<bool> UpdateMeasurementConfigAsync(MasterConfigType measurementType) {
            var flag = false;
            var masterConfig = await GetMasterConfigAsync();
            var update = Builders<MasterConfig>.Update.Set("MeasurementConfig." + measurementType.Type, measurementType.IsActive);
            flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, update);
            if (flag && !measurementType.IsActive) {
                var count = masterConfig.Measurements.Where(f => f.Type == measurementType.Type.ToLower()).Count();
                if (count > 0) {
                    var remove = Builders<MasterConfig>.Update.PullFilter(c => c.Measurements, f => f.Type == measurementType.Type.ToLower());
                    flag = await _repository.UpdateOneAsync<MasterConfig>(masterConfig, remove);
                }
            }

            return flag;
        }
      
        #endregion
    }
}
