using CRM.Business.Labs;
using CRM.DAL.LabRepositories;
using CRM.Entities.MongoEntities.Lab;
using CRM.Entities.MongoEntities.LabCollection;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Supplier.Controllers.Labs {
    [Route("api/Lab")]
    [ApiController]
    [Authorize]
    public class LabsController : ControllerBase {
        private readonly ILabRepository _repository;

        public LabsController(ILabRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetLabAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetLabAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetLabsAsync() {
            var result = await _repository.GetLabsAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetLabsAsync([FromBody] LabSearchCriteria labCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetLabsAsync(labCriteria, skip, take);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "Lab", "Lab Inserted")]
        public async Task<IActionResult> InsertLabAsync([FromBody] Lab lab) {
            var result = await _repository.InsertLabAsync(lab);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "Lab", "Lab Updated")]
        public async Task<IActionResult> UpdateLabAsync([FromBody] Lab lab) {
            if (string.IsNullOrEmpty(lab.Id))
                return BadRequest("Lab Id is required!");

            var result = await _repository.UpdateLabAsync(lab);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "Lab Deleted")]
        public async Task<IActionResult> DeleteLabAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Lab is required!");

            var result = await _repository.DeleteLabAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertLabIssue")]
        [DbLogger(LogDataType.Object, "Lab", "Lab Issue Inserted")]
        public async Task<IActionResult> InsertLabIssue([FromBody] LabIssue labIssue) {
            var result = await _repository.InsertLabIssue(labIssue);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllLabIssue")]
        public async Task<IActionResult> GetLabsIssueAsync() {
            var result = await _repository.GetLabsIssueAsync();

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateLabReceive")]
        public async Task<IActionResult> UpdateLabReceiveAsync([FromBody] IList<LabReceiveItem> labReceiveItem) {
            var result = await _repository.UpdateLabReceiveAsync(labReceiveItem);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetGradingFromStoneIds")]
        public async Task<IActionResult> GetGradingFromStoneIdsAsync([FromBody] IList<string> ids) {
            var result = await _repository.GetGradingFromStoneIdsAsync(ids);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetInvItemsFromStoneIds")]
        public async Task<IActionResult> GetInvItemsFromStoneIdsAsync([FromBody] IList<string> ids) {
            var result = await _repository.GetInvItemsFromStoneIdsAsync(ids);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertLabResult")]
        [DbLogger(LogDataType.Object, "Lab", "Lab Result Inserted")]
        public async Task<IActionResult> InsertLabResult([FromBody] IList<LabResult> labResult) {
            var result = await _repository.InsertLabResult(labResult);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertLabRecheckIssue")]
        [DbLogger(LogDataType.Object, "Lab", "Lab Recheck Issue Inserted")]
        public async Task<IActionResult> InsertLabRecheckIssue([FromBody] LabIssue labIssue) {
            var result = await _repository.InsertLabRecheckIssueAsync(labIssue);

            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateControllerNoFromStoneIds")]
        public async Task<IActionResult> UpdateControllerNoFromStoneIds([FromBody] IList<string> ids) {
            var result = await _repository.UpdateControllerNoFromStoneIds(ids);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetStoneIdsExistOrNotLabResult")]
        public async Task<IActionResult> GetStoneIdsExistOrNotLabResultAsync([FromBody] IList<string> ids) {
            var result = await _repository.GetStoneIdsExistOrNotLabResultAsync(ids);

            return Ok(result);
        }

    }
}

