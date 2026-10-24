using CRM.DAL.ConfigRepositories;
using CRM.Entities.MongoEntities.Configuration;
using CRM.Entities.MongoEntities.Configuration.DNorm;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Configuration {
    [Route("api/MasterConfig")]
    [ApiController]
    [Authorize]
    public class MasterConfigController : ControllerBase {
        private readonly IMasterConfigRepository _repository;

        public MasterConfigController(IMasterConfigRepository repository) {
            _repository = repository;
        }

        #region Get
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetMasterConfigAsync() {
            var result = await _repository.GetMasterConfigAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("Get/Inclusions")]
        public async Task<IActionResult> GetAllInlusionsAsync() {
            var result = await _repository.GetAllInclusionAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("Get/Measurements")]
        public async Task<IActionResult> GetAllMeasurementsAsync() {
            var result = await _repository.GetAllMeasurementAsync();

            return Ok(result);
        }
        #endregion

        #region Insert
        [HttpPost]
        [Route("InsertLab")]
        [DbLogger(LogDataType.Param, "lab", "Lab Inserted")]
        public async Task<IActionResult> InsertLabAsync([FromBody] MasterDNorm lab) {
            var result = await _repository.AddLabAsync(lab);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertShape")]
        [DbLogger(LogDataType.Param, "shape", "Shape Inserted")]
        public async Task<IActionResult> InsertShapeAsync([FromBody] MasterDNorm shape) {
            var result = await _repository.AddShapeAsync(shape);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertColors")]
        [DbLogger(LogDataType.Param, "colors", "Colors Inserted")]
        public async Task<IActionResult> InsertColorsAsync([FromBody] MasterDNorm colors) {
            var result = await _repository.AddColorsAsync(colors);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertClarities")]
        [DbLogger(LogDataType.Param, "clarities", "Clarities Inserted")]
        public async Task<IActionResult> InsertClaritiesAsync([FromBody] MasterDNorm clarities) {
            var result = await _repository.AddClaritiesAsync(clarities);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertCPS")]
        [DbLogger(LogDataType.Param, "cps", "CPS Inserted")]
        public async Task<IActionResult> InsertCPSAsync([FromBody] MasterDNorm cps) {
            var result = await _repository.AddCPSAsync(cps);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertFluorescence")]
        [DbLogger(LogDataType.Param, "fluorescence", "Fluorescence Inserted")]
        public async Task<IActionResult> InsertFluorescenceAsync([FromBody] MasterDNorm fluorescence) {
            var result = await _repository.AddFluorescenceAsync(fluorescence);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertCut")]
        [DbLogger(LogDataType.Param, "cut", "Cut Inserted")]
        public async Task<IActionResult> InsertCutAsync([FromBody] MasterDNorm cut) {
            var result = await _repository.AddCutAsync(cut);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertCutDetails")]
        [DbLogger(LogDataType.Param, "cutDetails", "Cut Details Inserted")]
        public async Task<IActionResult> InsertCutDetailsAsync([FromBody] List<CutDetailDNorm> cutDetails) {
            var result = await _repository.AddCutDetailsAsync(cutDetails);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertInclusion")]
        [DbLogger(LogDataType.Param, "inclusion", "Inclusion Inserted")]
        public async Task<IActionResult> InsertInclusionAsync([FromBody] MasterDNorm inclusion) {
            var result = await _repository.AddInclusionAsync(inclusion);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertMeasurement")]
        [DbLogger(LogDataType.Param, "Measurement", "Measurement Inserted")]
        public async Task<IActionResult> InsertMeasurementAsync([FromBody] MasterDNorm measurement) {
            var result = await _repository.AddMeasurementAsync(measurement);

            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("UpdateLab")]
        [DbLogger(LogDataType.Param, "lab", "Lab Updated")]
        public async Task<IActionResult> UpdateLabAsync([FromBody] MasterDNorm lab) {
            if (string.IsNullOrEmpty(lab.Id))
                return BadRequest("Lab id is required!");

            var result = await _repository.UpdateLabAsync(lab);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateShape")]
        [DbLogger(LogDataType.Param, "shape", "Shape Updateed")]
        public async Task<IActionResult> UpdateShapeAsync([FromBody] MasterDNorm shape) {
            if (string.IsNullOrEmpty(shape.Id))
                return BadRequest("Shape id is required!");

            var result = await _repository.UpdateShapeAsync(shape);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateColors")]
        [DbLogger(LogDataType.Param, "colors", "Colors Updateed")]
        public async Task<IActionResult> UpdateColorsAsync([FromBody] MasterDNorm colors) {
            if (string.IsNullOrEmpty(colors.Id))
                return BadRequest("Colors id is required!");

            var result = await _repository.UpdateColorsAsync(colors);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateClarities")]
        [DbLogger(LogDataType.Param, "clarities", "Clarities Updateed")]
        public async Task<IActionResult> UpdateClaritiesAsync([FromBody] MasterDNorm clarities) {
            if (string.IsNullOrEmpty(clarities.Id))
                return BadRequest("Clarities id is required!");

            var result = await _repository.UpdateClaritiesAsync(clarities);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateCPS")]
        [DbLogger(LogDataType.Param, "cps", "CPS Updateed")]
        public async Task<IActionResult> UpdateCPSAsync([FromBody] MasterDNorm cps) {
            if (string.IsNullOrEmpty(cps.Id))
                return BadRequest("CPS id is required!");

            var result = await _repository.UpdateCPSAsync(cps);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateFluorescence")]
        [DbLogger(LogDataType.Param, "fluorescence", "Fluorescence Updateed")]
        public async Task<IActionResult> UpdateFluorescenceAsync([FromBody] MasterDNorm fluorescence) {
            if (string.IsNullOrEmpty(fluorescence.Id))
                return BadRequest("Fluorescence id is required!");

            var result = await _repository.UpdateFluorescenceAsync(fluorescence);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateCut")]
        [DbLogger(LogDataType.Param, "cut", "Cut Updateed")]
        public async Task<IActionResult> UpdateCutAsync([FromBody] MasterDNorm cut) {
            if (string.IsNullOrEmpty(cut.Id))
                return BadRequest("Cut id is required!");

            var result = await _repository.UpdateCutAsync(cut);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInclusion")]
        [DbLogger(LogDataType.Param, "inclusion", "Inclusion Updated")]
        public async Task<IActionResult> UpdateInclusionAsync([FromBody] MasterDNorm inclusion) {
            if (string.IsNullOrEmpty(inclusion.Id))
                return BadRequest("Inclusion id is required!");

            var result = await _repository.UpdateInclusionAsync(inclusion);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInclusionActive")]
        [DbLogger(LogDataType.Param, "inclusion active", "Inclusion active Updated")]
        public async Task<IActionResult> UpdateInclusionActiveAsync([FromBody] MasterConfigType inclusionType) {

            var result = await _repository.UpdateInclusionConfigAsync(inclusionType);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateMeasurement")]
        [DbLogger(LogDataType.Param, "Measurement", "Measurement Updated")]
        public async Task<IActionResult> UpdateMeasurementAsync([FromBody] MasterDNorm measurement) {
            if (string.IsNullOrEmpty(measurement.Id))
                return BadRequest("Measurment id is required!");

            var result = await _repository.UpdateMeasurementAsync(measurement);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateMeasurementActive")]
        [DbLogger(LogDataType.Param, "Measurement active", "Measurement active Updated")]
        public async Task<IActionResult> UpdateMeasurementActiveAsync([FromBody] MasterConfigType measurementType) {

            var result = await _repository.UpdateMeasurementConfigAsync(measurementType);
            return Ok(result);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("DeleteLab/{labId}")]
        [DbLogger(LogDataType.Param, "labId", "Lab Deleted")]
        public async Task<IActionResult> DeleteLabAsync(string labId) {
            if (string.IsNullOrEmpty(labId))
                return BadRequest("Lab id is required!");

            var result = await _repository.DeleteLabAsync(labId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteShape/{shapeId}")]
        [DbLogger(LogDataType.Param, "shapeId", "Shape Deleted")]
        public async Task<IActionResult> DeleteShapeAsync(string shapeId) {
            if (string.IsNullOrEmpty(shapeId))
                return BadRequest("Shape id is required!");

            var result = await _repository.DeleteShapeAsync(shapeId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteColors/{colorsId}")]
        [DbLogger(LogDataType.Param, "colorsId", "Colors Deleted")]
        public async Task<IActionResult> DeleteColorsAsync(string colorsId) {
            if (string.IsNullOrEmpty(colorsId))
                return BadRequest("Colors id is required!");

            var result = await _repository.DeleteColorsAsync(colorsId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteClarities/{claritiesId}")]
        [DbLogger(LogDataType.Param, "claritiesId", "Clarities Deleted")]
        public async Task<IActionResult> DeleteClaritiesAsync(string claritiesId) {
            if (string.IsNullOrEmpty(claritiesId))
                return BadRequest("Clarities id is required!");

            var result = await _repository.DeleteClaritiesAsync(claritiesId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCPS/{cpsId}")]
        [DbLogger(LogDataType.Param, "cpsId", "CPS Deleted")]
        public async Task<IActionResult> DeleteCPSAsync(string cpsId) {
            if (string.IsNullOrEmpty(cpsId))
                return BadRequest("CPS id is required!");

            var result = await _repository.DeleteCPSAsync(cpsId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteFluorescence/{fluorescenceId}")]
        [DbLogger(LogDataType.Param, "fluorescenceId", "Fluorescence Deleted")]
        public async Task<IActionResult> DeleteFluorescenceAsync(string fluorescenceId) {
            if (string.IsNullOrEmpty(fluorescenceId))
                return BadRequest("Fluorescence id is required!");

            var result = await _repository.DeleteFluorescenceAsync(fluorescenceId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCut/{cutId}")]
        [DbLogger(LogDataType.Param, "cutId", "Cut Deleted")]
        public async Task<IActionResult> DeleteCutAsync(string cutId) {
            if (string.IsNullOrEmpty(cutId))
                return BadRequest("Cut id is required!");

            var result = await _repository.DeleteCutAsync(cutId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCutDetails/{cutDetailsId}")]
        [DbLogger(LogDataType.Param, "cutDetailsId", "Cut Details Deleted")]
        public async Task<IActionResult> DeleteCutDetailsAsync(string cutDetailsId) {
            if (string.IsNullOrEmpty(cutDetailsId))
                return BadRequest("Cut Details id is required!");

            var result = await _repository.DeleteCutDetailsAsync(cutDetailsId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteInclusion/{inclusionId}")]
        [DbLogger(LogDataType.Param, "InclusionId", "Inclusion Deleted")]
        public async Task<IActionResult> DeleteInclusionAsync(string inclusionId) {
            if (string.IsNullOrEmpty(inclusionId))
                return BadRequest("Inclusion id is required!");

            var result = await _repository.DeleteInclusionAsync(inclusionId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteMeasurement/{measurementId}")]
        [DbLogger(LogDataType.Param, "MeasurementID", "Measurement Deleted")]
        public async Task<IActionResult> DeleteMeasurementAsync(string measurementId) {
            if (string.IsNullOrEmpty(measurementId))
                return BadRequest("Measurement id is required!");

            var result = await _repository.DeleteMeasurementAsync(measurementId);
            return Ok(result);
        }
        #endregion

    }
}
