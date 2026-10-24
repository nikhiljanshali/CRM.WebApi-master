using CRM.Logging.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRM.Logging.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using CRM.Entities.MongoEntities.Organization;
using CRM.Business.Organizations;
using Microsoft.AspNetCore.Authorization;
using CRM.DAL.OrganizationRepositories;

namespace CRM.Customer.Controllers.Organizations {
    [Route("api/Organization")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase {
        private readonly IOrganizationRepository _repository;

        public OrganizationController(IOrganizationRepository repository) {
            _repository = repository;
        }
      
        [HttpGet]
        [Route("GetDNorms")]
        public async Task<IActionResult> GetOrganizationDNormsAsync() {
            var result = await _repository.GetOrganizationDNormsAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("SearchDNorms/{name}")]
        public async Task<IActionResult> GetOrganizationDNormsAsync(string name) {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name is required!");

            var result = await _repository.GetOrganizationDNormsAsync(name);
            return Ok(result);
        }

      
        [HttpGet]
        [Route("GetBranchDNormByOrganization/{organizationId}")]
        public async Task<IActionResult> GetBranchDNormByOrganizationAsync(string organizationId) {
            if (string.IsNullOrEmpty(organizationId))
                return BadRequest("Organization Id is required!");

            var result = await _repository.GetBranchDNormsAsync(organizationId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDepartmentByOrganization/{organizationId}")]
        public async Task<IActionResult> GetDepartmentByOrganizationAsync(string organizationId) {
            if (string.IsNullOrEmpty(organizationId))
                return BadRequest("Organization Id is required!");

            var result = await _repository.GetDepartmentsAsync(organizationId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOrganizationAddressByEmployeeIdAsync/{employeeId}")]
        public async Task<IActionResult> GetOrganizationAddressByEmployeeIdAsync(string employeeId) {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest("Employee Id is required!");

            var result = await _repository.GetOrgAddressByEmployeeAsync(employeeId);
            return Ok(result);
        }
    }
}
