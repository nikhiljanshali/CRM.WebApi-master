using CRM.Business.Customers;
using CRM.DAL.CustomerRepositories;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Customers {
    [Route("api/CustomerVerification")]
    [ApiController]
    [Authorize]
    public class CustomerVerificationController :ControllerBase {
        private readonly ICustomerVerificationRepository _repository;

        public CustomerVerificationController(ICustomerVerificationRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetRequests/{status}")]
        public async Task<IActionResult> GetRequestAsync(string status) {
            var result = await _repository.GetAllCustomersAsync(status);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetCustomerVerificationsAsync([FromBody]CustomerVerificationSearchCriteria CustomerVerificationCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetCustomersAsync(CustomerVerificationCriteria, skip, take);
            return Ok(result);
        }

        [HttpPost]
        [Route("AcceptOrRejectCustomer/{isAccept}")]
        public async Task<IActionResult> AcceptOrRejectCustomersAsync([FromBody] Customer customer, bool isAccept) {
            bool flag = await _repository.ExistCustomerAsync(customer);
            if (flag)
                return BadRequest("Customer is already registered");

            var result = await _repository.AcceptOrRejectCustomersAsync(customer, isAccept);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetSupplierDNorm")]
        public async Task<IActionResult> GetSupplierDNormsAsync() {
            var result = await _repository.GetSupplierDNormsAsync();
            return Ok(result);
        }

    }
}
