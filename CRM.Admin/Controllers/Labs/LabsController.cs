using CRM.Business.Labs;
using CRM.DAL.LabRepositories;
using CRM.Entities.MongoEntities.LabCollection;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Labs {
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
    }
}
