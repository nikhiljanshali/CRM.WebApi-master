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
    [Route("api/Config")]
    [ApiController]
    [Authorize]
    public class ConfigController : ControllerBase {

        private readonly IConfigRepository _repository;
        public ConfigController(IConfigRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetGridConfig/{empid}/{pageName}/{gridName}")]
        public async Task<IActionResult> GetGridConfigAsync(string empid, string pageName, string gridName) {
            if (string.IsNullOrEmpty(empid))
                return BadRequest("Id is required!");

            var result = await _repository.GetGridConfigAsync(empid, pageName,gridName);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetMasterGridConfig/{pageName}/{gridName}")]
        public async Task<IActionResult> GetMasterGridConfigAsync(string pageName, string gridName) {
            if (string.IsNullOrEmpty(pageName))
                return BadRequest("PageName is required!");

            var result = await _repository.GetMasterGridConfigAsync(pageName, gridName);
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertGridConfig")]
        [DbLogger(LogDataType.Param, "GridConfig", "GridConfig Inserted")]
        public async Task<IActionResult> InsertGridConfigAsync([FromBody] GridConfig gridConfig) {
            var result = await _repository.InsertGridConfigAsync(gridConfig);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertMasterGridConfig")]
        [DbLogger(LogDataType.Param, "GridMasterConfig", "GridMasterConfig Inserted")]
        public async Task<IActionResult> InsertMasterGridConfigAsync([FromBody] GridMasterConfig gridMasterConfig) {
            var result = await _repository.InsertMasterGridConfigAsync(gridMasterConfig);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateGridConfig")]
        [DbLogger(LogDataType.Object, "GridConfig", "GridConfig Updated")]
        public async Task<IActionResult> UpdateGridConfigAsync([FromBody] GridConfig gridConfig) {
            if (string.IsNullOrEmpty(gridConfig.Id))
                return BadRequest("GridConfig id is required!");

            var result = await _repository.UpdateGridConfigAsync(gridConfig);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateMasterGridConfig")]
        [DbLogger(LogDataType.Object, "GridMasterConfig", "GridMasterConfig Updated")]
        public async Task<IActionResult> UpdateMasterGridConfigAsync([FromBody] GridMasterConfig gridMasterConfig) {
            if (string.IsNullOrEmpty(gridMasterConfig.Id))
                return BadRequest("GridConfig id is required!");

            var result = await _repository.UpdateMasterGridConfigAsync(gridMasterConfig);

            return Ok(result);
        }

    }
}
