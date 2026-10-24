using CRM.DAL.OrganizationRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Organizations {
    [Route("api/Customer")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetCustomerAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetCustomerAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetCustomersAsync() {
            var result = await _repository.GetCustomersAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll/{name}")]
        public async Task<IActionResult> GetCustomersAsyc(string name) {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name is required!");

            var result = await _repository.GetCustomersAsync(name);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetPaginatedCustomersAsync(int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetCustomersAsync(skip, take);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "customer", "Customer Inserted")]
        public async Task<IActionResult> InsertCustomerAsync([FromBody] Customer customer) {
            var result = await _repository.InsertCustomerAsync(customer);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertAddress/{customerId}")]
        [DbLogger(LogDataType.Object, "customerId,address", "Customer Address Inserted")]
        public async Task<IActionResult> InsertAddressAsync(string customerId, [FromBody] Address address) {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("Customer Id is required!");

            var result = await _repository.InsertAddressAsync(customerId, address);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "customer", "Customer Updated")]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] Customer customer) {
            var result = await _repository.UpdateCustomerAsync(customer);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "Customer Deleted")]
        public async Task<IActionResult> DeleteCustomerAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteCustomerAsync(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteAddress/{customerId}/{addressId}")]
        [DbLogger(LogDataType.Param, "customerId,addressId", "Customer Address Deleted")]
        public async Task<IActionResult> DeleteAddressAsync(string customerId, string addressId) {
            if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(addressId))
                return BadRequest("Customer id and Address id is required!");

            var result = await _repository.DeleteAddressAsync(customerId, addressId);
            return Ok(result);
        }

    }
}
