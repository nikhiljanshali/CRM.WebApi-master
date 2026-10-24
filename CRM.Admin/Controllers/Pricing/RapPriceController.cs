using CRM.DAL.PricingRepositories;
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

namespace CRM.Admin.Controllers.Pricing {
    [Route("api/RapPrice")]
    [ApiController]
    [Authorize]
    public class RapPriceController : ControllerBase {
        private readonly IRapPriceRepository _repository;

        public RapPriceController(IRapPriceRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllRapPriceAsync() {
            var result = await _repository.GetAllRapPriceAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("UploadRapPriceFile")]
        [DbLogger(LogDataType.Object, "data", "UploadRapPrice Inserted")]
        public async Task<IActionResult> UploadRapPriceFile([FromBody] IList<RapPrice> data) {
            var result = await _repository.UpdateRapPrice(data);

            return Ok(result);
        }
    }
}
