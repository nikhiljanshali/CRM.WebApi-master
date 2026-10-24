using CRM.Business.MarketSheets;
using CRM.DAL.MarketSheetRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Seller.Controllers.MarketSheets {
    [Route("api/Marketsheet")]
    [ApiController]
    [Authorize]
    public class MarketSheetController : ControllerBase {
        private readonly IMarketSheetSellerRepository _repository;

        public MarketSheetController(IMarketSheetSellerRepository repository) {
            _repository = repository;
        }


        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetMarketSheetsAsync([FromBody] MarketSheetSellerSearchCriteria MarketSheetSellerCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetMarketSheetsAsync(MarketSheetSellerCriteria, skip, take);
            return Ok(result);
        }
       
    }
}
