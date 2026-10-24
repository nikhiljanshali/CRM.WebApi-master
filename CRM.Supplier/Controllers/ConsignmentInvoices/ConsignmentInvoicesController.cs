using CRM.DAL.StockOnHandRepositories;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Supplier.Controllers.ConsignmentInvoices {
    [Route("api/ConsignmentInvoice")]
    [ApiController]
    [Authorize]
    public class ConsignmentInvoicesController : ControllerBase {
        private readonly IStockOnHandRepository _repository;
        public ConsignmentInvoicesController(IStockOnHandRepository repository) {
            _repository = repository;
        }
             

        [HttpGet]
        [Route("GetAllConsignmentInvoices")]
        public async Task<IActionResult> GetConsignmentInvoicesAsync() {
            var result = await _repository.GetConsignmentInvoicesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetStockOnHandByStoneIdAsync/{stoneId}")]
        public async Task<IActionResult> GetStockOnHandByStoneIdAsync(string stoneId) {
            var result = await _repository.GetStockOnHandByStoneIdAsync(stoneId);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertConsignmentInvoice")]
        [DbLogger(LogDataType.Object, "ConsignmentInvoice", "Consignment Invoice Inserted")]
        public async Task<IActionResult> InsertLabIssue([FromBody] ConsignmentInvoice consignmentInvoice) {
            var result = await _repository.InsertConsignmentInvoice(consignmentInvoice);

            return Ok(result);
        }
    }
}
