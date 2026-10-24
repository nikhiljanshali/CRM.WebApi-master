using CRM.Business.Inventory;
using CRM.DAL.StockOnHandRepositories;
using CRM.Entities.MongoEntities.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Supplier.Controllers.StockOnHand {
    [Route("api/StockOnHand")]
    [ApiController]
    [Authorize]
    public class StockOnHandController : ControllerBase {
        private readonly IStockOnHandRepository _repository;
        public StockOnHandController(IStockOnHandRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetStockOnHandAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");
            var result = await _repository.GetStockOnHandAsync(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetStockOnHandAsync() {
            var result = await _repository.GetStockOnHandAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetStockOnHandAsync([FromBody] StockOnHandSearchCriteria StockOnHandSearchCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetStockOnHandAsync(StockOnHandSearchCriteria, skip, take);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateStock")]
        public async Task<IActionResult> UpdateStockAsync([FromBody] IList<InventoryItems> inventoryItems) {
            var result = await _repository.UpdateStockAsync(inventoryItems);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdatePricingRequest")]
        public async Task<IActionResult> UpdatePricingRequestAsync([FromBody] IList<InventoryItems> inventoryItems) {
            var result = await _repository.UpdatePricingRequestAsync(inventoryItems);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInventoryMarketSheet")]
        public async Task<IActionResult> UpdateInventoryMarketSheetAsync([FromBody] IList<InventoryItems> inventoryItems) {
            var result = await _repository.UpdateInventoryMarketSheetAsync(inventoryItems);

            return Ok(result);
        }

    }
}