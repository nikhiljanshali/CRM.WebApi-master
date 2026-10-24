using CRM.Business.Pricing;
using CRM.DAL.PricingRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Pricing;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Pricing.Controllers.PriceRequest {
    [Route("api/PriceRequest")]
    [ApiController]
    [Authorize]
    public class PriceRequestController : ControllerBase {
        private readonly IPriceRequestRepository _repository;

        public PriceRequestController(IPriceRequestRepository repository) {
            _repository = repository;
        }

        [HttpPost]
        [Route("Get")]
        public async Task<IActionResult> GetPricingRequestData([FromBody] PriceRequestApiModel request) {
            var result = await _repository.GetPriceRequestDataAsync(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetPricingHsitory/{stoneId}")]
        public async Task<IActionResult> GetPricingHsitoryData(string stoneId) {
            if (string.IsNullOrEmpty(stoneId))
                return BadRequest("StoneId is required!");

            var result = await _repository.GetPricingHistoryByStoneAsync(stoneId);
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertSingleTempPricing")]
        [DbLogger(LogDataType.Object, "pricing", "Insert Temp Pricing data")]
        public async Task<IActionResult> InsertTempPricing([FromBody] TempPricing pricing) {
            var result = await _repository.InsertTempPrice(pricing);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertTempPricing")]
        [DbLogger(LogDataType.Object, "pricing", "Insert Temp Pricing datas")]
        public async Task<IActionResult> InsertTempPricings([FromBody] IList<TempPricing> pricing) {
            var result = await _repository.InsertTempPrices(pricing);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertSinglePricingRequest")]
        [DbLogger(LogDataType.Object, "pricing", "Insert Pricing Request data")]
        public async Task<IActionResult> InsertPricingRequest([FromBody] PricingRequest pricing) {
            var result = await _repository.InsertPricingRequest(pricing);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertPricingRequest")]
        [DbLogger(LogDataType.Object, "pricing", "Insert Pricing Request datas")]
        public async Task<IActionResult> InsertPricingRequests([FromBody] IList<PricingRequest> pricing) {
            var result = await _repository.InsertPricingRequests(pricing);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateTempPricing")]
        [DbLogger(LogDataType.Object, "pricing", "Update Temp Pricing data")]
        public async Task<IActionResult> UpdateTempPricing([FromBody] IList<TempPricing> pricing) {
            var result = await _repository.RemoveTempPrice(pricing);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdatePricingRequest")]
        [DbLogger(LogDataType.Object, "pricing", "Update Pricing Reqeust data")]
        public async Task<IActionResult> UpdatePricingRequest([FromBody] IList<PricingRequest> pricing) {
            var result = await _repository.RemovePricingRequest(pricing);

            return Ok(result);
        }
    }
}
