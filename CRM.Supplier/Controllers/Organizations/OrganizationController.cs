using CRM.Logging.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRM.Logging.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using CRM.DAL.OrganizationRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Business.Organizations;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Admin.Controllers.Organizations {
    [Route("api/Organization")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase {
        private readonly IOrganizationRepository _repository;

        public OrganizationController(IOrganizationRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetOrganizationAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetOrganizationAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("Search/{name}")]
        public async Task<IActionResult> GetOrganizationByNameAsync(string name) {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name is required!");

            var result = await _repository.GetOrganizationByNameAsync(name);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetOrganizationsAsync() {
            var result = await _repository.GetOrganizationsAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetOrganizationsAsync([FromBody] OrganizationSearchCriteria OrganizationCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetOrganizationsAsync(OrganizationCriteria,skip, take);
            return Ok(result);
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

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "organization", "Organization Inserted")]
        public async Task<IActionResult> InsertOrganizationAsync([FromBody] Organization organization) {
            var result = await _repository.InsertOrganizationAsync(organization);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertBranch/{organizationId}")]
        [DbLogger(LogDataType.Param, "organizationId,branch", "Branch Inserted")]
        public async Task<IActionResult> InsertBranchAsync(string organizationId, [FromBody] Branch branch) {
            if (string.IsNullOrEmpty(organizationId))
                return BadRequest("Organization id is required!");

            bool result = await _repository.ExistBranchAsync(organizationId, branch, true);
            if (result)
                return BadRequest("Branch name is already Exist");

            result = await _repository.InsertBranchAsync(organizationId, branch);
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertDepartment/{organizationId}")]
        [DbLogger(LogDataType.Param, "organizationId,department", "Department Inserted")]
        public async Task<IActionResult> InsertDepartmentAsync(string organizationId, [FromBody] Department department) {
            if (string.IsNullOrEmpty(organizationId))
                return BadRequest("Organization id is required!");

            bool result = await _repository.ExistDepartmentAsync(organizationId, department, true);
            if (result)
                return BadRequest("Department name is already Exist");

            result = await _repository.InsertDepartmentAsync(organizationId, department);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "organization", "Organization Updated")]
        public async Task<IActionResult> UpdateOrganizationAsync([FromBody] Organization organization) {
            var result = await _repository.UpdateOrganizationAsync(organization);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateBranch/{organizationId}")]
        [DbLogger(LogDataType.Object, "organizationId,branch", "Branch Updated")]
        public async Task<IActionResult> UpdateOrganizationBranchAsync(string organizationId, Branch branch) {
            if (string.IsNullOrEmpty(organizationId))
                return BadRequest("Organization id is required!");

            bool result = await _repository.ExistBranchAsync(organizationId, branch, false);
            if (result)
                return BadRequest("Branch name is already Exist");

            result = await _repository.UpdateOrganizationBranchAsync(organizationId, branch);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateDepartment/{organizationId}")]
        [DbLogger(LogDataType.Object, "organizationId,department", "Department Updated")]
        public async Task<IActionResult> UpdateOrganizationDepartmentAsync(string organizationId, Department department) {
            if (string.IsNullOrEmpty(organizationId))
                return BadRequest("Organization id is required!");

            bool result = await _repository.ExistDepartmentAsync(organizationId, department, false);
            if (result)
                return BadRequest("Department name is already Exist");

            result = await _repository.UpdateOrganizationDepartmentAsync(organizationId, department);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "Organization Deleted")]
        public async Task<IActionResult> DeleteOrganizationAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteOrganizationAsync(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteBranch/{organizationId}/{branchId}")]
        [DbLogger(LogDataType.Param, "organizationId,branchId", "Branch Deleted")]
        public async Task<IActionResult> DeleteOrganizationBranchAsync(string organizationId, string branchId) {
            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrEmpty(branchId))
                return BadRequest("Organization id and Branch id is required!");

            var result = await _repository.DeleteBranchAsync(organizationId, branchId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteDepartment/{organizationId}/{departmentId}")]
        [DbLogger(LogDataType.Param, "organizationId,departmentId", "Department Deleted")]
        public async Task<IActionResult> DeleteOrganizationdepartmentIdAsync(string organizationId, string departmentId) {
            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrEmpty(departmentId))
                return BadRequest("Organization id and Department id is required!");

            var result = await _repository.DeleteDepartmentAsync(organizationId, departmentId);
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
