using CRM.Business.Logistics;
using CRM.DAL.LogisticRepositories;
using CRM.Entities.MongoEntities.LogisticCollection;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Supplier.Controllers.Logistics {
    [Route("api/Logistic")]
    [ApiController]
    [Authorize]
    public class LogisticsController : ControllerBase {
        private readonly ILogisticRepository _repository;

        public LogisticsController(ILogisticRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetLogisticAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetLogisticAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetLogisticsAsync() {
            var result = await _repository.GetLogisticsAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetLogisticsAsync([FromBody] LogisticSearchCriteria logisticCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetLogisticsAsync(logisticCriteria, skip, take);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "Logistic", "Logistic Inserted")]
        public async Task<IActionResult> InsertLogisticAsync([FromBody] Logistic logistic) {
            var result = await _repository.InsertLogisticAsync(logistic);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "Logistic", "Logistic Updated")]
        public async Task<IActionResult> UpdateLogisticAsync([FromBody] Logistic logistic) {
            if (string.IsNullOrEmpty(logistic.Id))
                return BadRequest("Logistic Id is required!");

            var result = await _repository.UpdateLogisticAsync(logistic);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "Logistic Deleted")]
        public async Task<IActionResult> DeleteLogisticAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Logistic Id  is required!");

            var result = await _repository.DeleteLogisticAsync(id);
            return Ok(result);
        }
    }
}
