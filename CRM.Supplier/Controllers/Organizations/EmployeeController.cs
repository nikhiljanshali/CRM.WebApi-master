using CRM.Business.Employees;
using CRM.DAL.OrganizationRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Organizations {
    [Route("api/Employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetEmployeeAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetEmployeeAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetEmployeesAsync() {
            var result = await _repository.GetEmployeesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll/{isActive}")]
        public async Task<IActionResult> GetEmployeesAsync(bool isActive) {
            var result = await _repository.GetEmployeesAsync(isActive);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetEmployeeAsync([FromBody] EmployeeSearchCriteria EmployeeCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetEmployeesAsync(EmployeeCriteria, skip, take);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("GetAll/{departmentId}")]
        //public async Task<IActionResult> GetEmployeesAsync(string departmentId) {
        //    if (string.IsNullOrEmpty(departmentId))
        //        return BadRequest("Department Id is required!");

        //    var result = await _repository.GetEmployeesAsync(departmentId);
        //    return Ok(result);
        //}

        [HttpGet]
        [Route("GetByOrigin/{origin}")]
        public async Task<IActionResult> GetEmployeesByOrigin(string origin) {
            if (string.IsNullOrEmpty(origin))
                return BadRequest("Origin is required!");

            var result = await _repository.GetEmployeesbyOriginAsync(origin);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "employee", "Employee Inserted")]
        public async Task<IActionResult> InsertEmployeeAsync([FromBody] Employee employee) {
            var result = await _repository.InsertEmployeeAsync(employee);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "employee", "Employee Updated")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] Employee employee) {
            var result = await _repository.UpdateEmployeeAsync(employee);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "Employee Deleted")]
        public async Task<IActionResult> DeleteEmployeeAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteEmployeeAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("Credential/{id}")]
        public async Task<IActionResult> GetUserCredentialAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetUserCredentialAsync(id);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetEmpFromDept/{departmentId}")]
        public async Task<IActionResult> GetEmpFromDeptAsync(string departmentId) {
            if (string.IsNullOrEmpty(departmentId))
                return BadRequest("Department Id is required!");

            var result = await _repository.GetEmpFromDeptAsync(departmentId);
            return Ok(result);
        }

    }
}
