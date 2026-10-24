using CRM.DAL.OrganizationRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Customer.Controllers.Organizations {
    [Route("api/Customer")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Credential/{id}")]
        public async Task<IActionResult> GetUserCredentialAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetCustomerCredentialAsync(id);
            return Ok(result);
        }
    }
}
