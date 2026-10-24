using CRM.Business.MarketSheets;
using CRM.DAL.MarketSheetRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Customer.Controllers.MarketSheets {
    [Route("api/Marketsheet")]
    [ApiController]
    [Authorize]
    public class MarketSheetController : ControllerBase {
        private readonly IMarketSheetRepository _repository;

        public MarketSheetController(IMarketSheetRepository repository) {
            _repository = repository;
        }

        [HttpPost]
        [Route("BlockClients/{InventoryId}")]
        public async Task<IActionResult> SetBlockClientsAsync(string InventoryId,[FromBody] IList<string> BlockClientIds) {
            if (string.IsNullOrEmpty(InventoryId))
                return BadRequest("InventoryId is Required!");

            var result = await _repository.SetBlockClients(InventoryId, BlockClientIds);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetMarketSheetsAsync([FromBody] MarketSheetSearchCriteria MarketSheetCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetMarketSheetsAsync(MarketSheetCriteria, skip, take);
            return Ok(result);
        }
       
    }
}
