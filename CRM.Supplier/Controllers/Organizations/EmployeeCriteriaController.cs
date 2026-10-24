using CRM.DAL.OrganizationRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.Supplier.Controllers.Organizations {
    [Route("api/EmployeeCriteria")]
    [ApiController]
    [Authorize]
    public class EmployeeCriteriaController : ControllerBase {
        private readonly IEmployeeCriteriaRepository _repository;

        public EmployeeCriteriaController(IEmployeeCriteriaRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetEmployeeCriteriaAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetEmployeeCriteriaAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetEmployeeCriteriaAsync() {
            var result = await _repository.GetEmployeeCriteriaAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetByEmployee/{id}")]
        public async Task<IActionResult> GetEmployeeCriteriaByEmployeeAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetEmployeeCriteriaByEmployeeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "employeeCriteria", "Employee Criteria Inserted")]
        public async Task<IActionResult> InsertEmployeeCriteriaAsync([FromBody] EmployeeCriteria employeeCriteria) {
            var result = await _repository.InsertEmployeeCriteriaAsync(employeeCriteria);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "employeeCriteria", "Employee Criteria Updated")]
        public async Task<IActionResult> UpdateEmployeeCriteriaAsync([FromBody] EmployeeCriteria employeeCriteria) {
            var result = await _repository.UpdateEmployeeCriteriaAsync(employeeCriteria);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "Employee Criteria Deleted")]
        public async Task<IActionResult> DeleteEmployeeCriteriaAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteEmployeeCriteriaAsync(id);
            return Ok(result);
        }
    }
}
