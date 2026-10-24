using CRM.Business.Inventory;
using CRM.DAL.InventoryRepositories;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Inventory {
    [Route("api/Inventory")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase {
        private readonly IInventoryRepository _repository;

        public InventoryController(IInventoryRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetInventoryAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetInventoryAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetByStoneId/{stoneId}")]
        public async Task<IActionResult> GetInventoryByStoneIdAsync(string stoneId) {
            if (string.IsNullOrEmpty(stoneId))
                return BadRequest("Stone Id is required!");

            var result = await _repository.GetInventoryByStoneIdAsync(stoneId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetInventoriesAsync() {
            var result = await _repository.GetInventoriesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetInventoriesWithOutRFID")]
        public async Task<IActionResult> GetInventoriesWithOutRFIDAsync() {
            var result = await _repository.GetInventoriesWithOutRFIDAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("GetStoneIdsExistOrNotAsync")]
        public async Task<IActionResult> GetStoneIdsExistOrNotAsync([FromBody] IList<string> ids) {
            var result = await _repository.GetStoneIdsExistOrNotAsync(ids);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetInventoriesAsync([FromBody] InventorySearchCriteria InventoryCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetInventoriesAsync(InventoryCriteria, skip, take);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "inventory", "Inventory Inserted")]
        public async Task<IActionResult> InsertInventoryAsync([FromBody] InventoryItems inventory) {
            var result = await _repository.InsertInventoryAsync(inventory);

            return Ok(result);
        }

        [HttpPost]
        [Route("UploadInventoryFile")]
        [DbLogger(LogDataType.Object, "InventoryExcelItems", "UploadInventory Inserted")]
        public async Task<IActionResult> UploadInventoryFile([FromBody] IList<InventoryExcelItems> InventoryExcelItems) {
            var result = await _repository.UploadInventoryFileAsync(InventoryExcelItems);

            return Ok(result);
        }

        [HttpPost]
        [Route("UploadInclusionFile")]
        [DbLogger(LogDataType.Object, "InclusionExcelItems", "UploadInclusion Inserted")]
        public async Task<IActionResult> UploadInclusionFile([FromBody] IList<InclusionExcelItems> InclusionExcelItems) {
            var result = await _repository.UploadInclusionFileAsync(InclusionExcelItems);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "inventory", "Inventory Updated")]
        public async Task<IActionResult> UpdateInventoryAsync([FromBody] InventoryItems inventory) {
            if (string.IsNullOrEmpty(inventory.Id))
                return BadRequest("Inventory id is required!");

            var result = await _repository.UpdateInventoryAsync(inventory);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInventoryItems")]
        [DbLogger(LogDataType.Object, "inventory", "Inventory Items Updated")]
        public async Task<IActionResult> UpdateInventoryItemsAsync([FromBody] IList<InventoryItems> inventoryItems) {
            var result = await _repository.UpdateInventoryItemsAsync(inventoryItems);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateMeasurement/{inventoryId}")]
        [DbLogger(LogDataType.Object, "inventoryId,measurement", "Measurement Updated")]
        public async Task<IActionResult> UpdateInventoryMeasurementAsync(string inventoryId, [FromBody] InventoryItemMeasurement measurement) {
            if (string.IsNullOrEmpty(inventoryId))
                return BadRequest("Inventory id is required!");

            var result = await _repository.UpdateInventoryMeasurementAsync(inventoryId, measurement);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInclusion/{inventoryId}")]
        [DbLogger(LogDataType.Object, "inventoryId,inclusion", "Inclusion Updated")]
        public async Task<IActionResult> UpdateInventoryInclusionAsync(string inventoryId, [FromBody] InventoryItemInclusion inclusion) {
            if (string.IsNullOrEmpty(inventoryId))
                return BadRequest("Inventory id is required!");

            var result = await _repository.UpdateInventoryInclusionAsync(inventoryId, inclusion);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInventoryHold")]
        [DbLogger(LogDataType.Object, "inventories", "Inventory Hold Updated")]
        public async Task<IActionResult> UpdateInventoryHoldAsync([FromBody] IList<InventoryItems> inventories) {
            var result = await _repository.UpdateInventoryHoldAsync(inventories);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInventoryList")]
        [DbLogger(LogDataType.Object, "inventories", "Inventory List Updated")]
        public async Task<IActionResult> UpdateInventoryListAsync([FromBody] IList<InventoryItems> inventorieItems) {
            var result = await _repository.UpdateInventoryListAsync(inventorieItems);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "Inventory Deleted")]
        public async Task<IActionResult> DeleteInventoryAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteInventoryAsync(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteMeasurement/{inventoryId}")]
        [DbLogger(LogDataType.Param, "inventoryId", "Measurement Deleted")]
        public async Task<IActionResult> DeleteInventoryMeasurementAsync(string inventoryId) {
            if (string.IsNullOrEmpty(inventoryId))
                return BadRequest("Inventory id is required!");

            var result = await _repository.DeleteInventoryMeasurementAsync(inventoryId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteInclusion/{inventoryId}")]
        [DbLogger(LogDataType.Param, "inventoryId", "Inclusion Deleted")]
        public async Task<IActionResult> DeleteInventoryInclusionAsync(string inventoryId) {
            if (string.IsNullOrEmpty(inventoryId))
                return BadRequest("Inventory id is required!");

            var result = await _repository.DeleteInventoryInclusionAsync(inventoryId);
            return Ok(result);
        }
    }
}
