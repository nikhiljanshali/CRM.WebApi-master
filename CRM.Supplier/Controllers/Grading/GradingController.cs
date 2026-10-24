using CRM.DAL.GradingRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Business.Inventory;
using CRM.DAL.InventoryRepositories;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using CRM.Entities.MongoEntities.GradingMaster;

namespace CRM.Supplier.Controllers.Grading {
    [Route("api/Grading")]
    [ApiController]
    [Authorize]
    public class GradingController : Controller {
        private readonly IGradingRepository _repository;
        private readonly IInventoryRepository _repositoryInv;

        public GradingController(IGradingRepository repository, IInventoryRepository repositoryInv) {
            _repository = repository;
            _repositoryInv = repositoryInv;
        }

        [HttpGet]
        [Route("Get/{stoneId}")]
        public async Task<IActionResult> GetGradingAsync(string stoneId) {
            if (string.IsNullOrEmpty(stoneId))
                return BadRequest("Id is required!");

            var result = await _repository.GetGradingAsync(stoneId);
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertGrading/{RapVer}")]
        [DbLogger(LogDataType.Object, "InsertGrading", "Grading Inserted")]
        public async Task<IActionResult> InsertGrading([FromBody] IList<InventoryItems> InventoryItems,string RapVer) {
            var result = await _repository.InsertGradingAsync(InventoryItems, RapVer);

            return Ok(result);
        }


        [HttpPut]
        [Route("UpdateGrading")]
        [DbLogger(LogDataType.Object, "UpdateGrading", "Update Grading")]
        public async Task<IActionResult> UpdateGradingAsync([FromBody] GradingMaster GradingMaster) {
            if (string.IsNullOrEmpty(GradingMaster.Id))
                return BadRequest("GradingMaster id is required!");

            bool result = true;
            InventoryItems invItem = new InventoryItems();

            invItem = await _repository.UpdateGradingAsync(GradingMaster);

            if (invItem != null) {
                result = await _repositoryInv.UpdateInventoryAsync(invItem);
            }

            return Ok(result);
        }

    }
}
