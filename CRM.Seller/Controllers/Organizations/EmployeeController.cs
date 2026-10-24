using CRM.Business.Employees;
using CRM.DAL.OrganizationRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.Seller.Controllers.Organizations {
    [Route("api/Employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetEmployeesAsync() {
            var result = await _repository.GetEmployeesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetByOrigin/{origin}")]
        public async Task<IActionResult> GetEmployeesByOrigin(string origin) {
            if (string.IsNullOrEmpty(origin))
                return BadRequest("Origin is required!");

            var result = await _repository.GetEmployeesbyOriginAsync(origin);
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
