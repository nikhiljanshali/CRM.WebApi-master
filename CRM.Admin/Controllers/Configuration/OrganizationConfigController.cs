using CRM.DAL.ConfigRepositories;
using CRM.Entities.MongoEntities.Configuration;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Configuration {
    [Route("api/OrganizationConfig")]
    [ApiController]
    [Authorize]
    public class OrganizationConfigController : ControllerBase {

        private readonly IOrganizationConfigRepository _repository;

        public OrganizationConfigController(IOrganizationConfigRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetOrganizationConfigAsyncById(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetOrganizationConfigAsyncByOrganizationId(id);

            return Ok(result);
        }
        
        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Param, "OrganizationConfig", "OrganizationConfig Inserted")]
        public async Task<IActionResult> AddOrganizationConfigAsync([FromBody] OrganizationConfig OrganizationConfig) {
            var result = await _repository.AddOrganizationConfigAsync(OrganizationConfig);

            return Ok(result);
        }
        
        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Param, "OrganizationConfig", "OrganizationConfig Updated")]
        public async Task<IActionResult> UpdateOrganizationConfigAsync([FromBody] OrganizationConfig OrganizationConfig) {
            var result = await _repository.UpdateOrganizationConfigAsync(OrganizationConfig);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "OrganizationConfig Deleted")]
        public async Task<IActionResult> DeleteOrganizationConfigAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteOrganizationConfigAsync(id);
            return Ok(result);
        }

    }
}
