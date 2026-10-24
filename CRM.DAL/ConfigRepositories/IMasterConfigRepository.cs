using CRM.Entities.MongoEntities.Configuration;
using CRM.Entities.MongoEntities.Configuration.DNorm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public interface IMasterConfigRepository {
        Task<MasterConfig> GetMasterConfigAsync();
        Task<IList<MasterDNorm>> GetAllInclusionAsync();
        Task<IList<MasterDNorm>> GetAllMeasurementAsync();

        Task<string> AddMasterConfigAsync(MasterConfig masterConfig);
        Task<bool> AddLabAsync(MasterDNorm lab);
        Task<bool> AddShapeAsync(MasterDNorm shape);
        Task<bool> AddColorsAsync(MasterDNorm colors);
        Task<bool> AddClaritiesAsync(MasterDNorm clarities);
        Task<bool> AddCPSAsync(MasterDNorm cps);
        Task<bool> AddFluorescenceAsync(MasterDNorm fluorescence);
        Task<bool> AddCutAsync(MasterDNorm cut);
        Task<bool> AddCutDetailsAsync(List<CutDetailDNorm> cutDetails);
        Task<bool> AddInclusionAsync(MasterDNorm inclusion);
        Task<bool> AddMeasurementAsync(MasterDNorm measurement);


        Task<bool> UpdateLabAsync(MasterDNorm lab);
        Task<bool> UpdateShapeAsync(MasterDNorm shape);
        Task<bool> UpdateColorsAsync(MasterDNorm colors);
        Task<bool> UpdateClaritiesAsync(MasterDNorm clarities);
        Task<bool> UpdateCPSAsync(MasterDNorm cps);
        Task<bool> UpdateFluorescenceAsync(MasterDNorm fluorescence);
        Task<bool> UpdateCutAsync(MasterDNorm cut);
        Task<bool> UpdateInclusionAsync(MasterDNorm inclusion);
        Task<bool> UpdateInclusionConfigAsync(MasterConfigType inclusionType);
        Task<bool> UpdateMeasurementAsync(MasterDNorm measurement);
        Task<bool> UpdateMeasurementConfigAsync(MasterConfigType measurementType);


        Task<bool> DeleteLabAsync(string labId);
        Task<bool> DeleteShapeAsync(string shapeId);
        Task<bool> DeleteColorsAsync(string colorsId);
        Task<bool> DeleteClaritiesAsync(string claritiesId);
        Task<bool> DeleteCPSAsync(string cpsId);
        Task<bool> DeleteFluorescenceAsync(string fluorescenceId);
        Task<bool> DeleteCutAsync(string cutId);
        Task<bool> DeleteCutDetailsAsync(string cutDetailsId);
        Task<bool> DeleteInclusionAsync(string inclusionId);
        Task<bool> DeleteMeasurementAsync(string measurementId);

    }
}
