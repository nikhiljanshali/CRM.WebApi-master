using CRM.DAL.ConfigRepositories;
using CRM.Entities.MongoEntities.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.Admin.Controllers.Configuration {
    [Route("api/Permission")]
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase {
        private readonly IPermissionRepository _repository;

        public PermissionController(IPermissionRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Origin/GetAll")]
        public async Task<IActionResult> GetAllOriginPermissionsAsync() {
            var result = await _repository.GetAllOriginPermissionsAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("Origin/Get/{origin}")]
        public async Task<IActionResult> GetOriginPermissionAsync(string origin) {
            if (string.IsNullOrEmpty(origin))
                return BadRequest("Origin is required!");

            var result = await _repository.GetOriginPermissionsAsync(origin);
            return Ok(result);
        }

        [HttpGet]
        [Route("Department/Get/{departmentId}")]
        public async Task<IActionResult> GetDeptPermissionAsync(string departmentId) {
            if (string.IsNullOrEmpty(departmentId))
                return BadRequest("DepartmentId id is required!");

            var result = await _repository.GetDeptPermissionAsync(departmentId);
            return Ok(result);
        }

        [HttpGet]
        [Route("Employee/Get/{employeeId}")]
        public async Task<IActionResult> GetEmpPermissionAsync(string employeeId) {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest("Employee id is required!");

            var result = await _repository.GetEmpPermissionAsync(employeeId);
            return Ok(result);
        }

        [HttpPost]
        [Route("Origin/Insert")]
        public async Task<IActionResult> InsertOriginPermissionAsync(OriginPermission permission) {
           var result = await _repository.InsertOriginPermissionAsync(permission);

            return Ok(result);
        }

        [HttpPost]
        [Route("Department/Insert")]
        public async Task<IActionResult> InsertDeptPermissionAsync(DeptPermission permission) {
            var result = await _repository.InsertDeptPermissionAsync(permission);

            return Ok(result);
        }

        [HttpPost]
        [Route("Employee/Insert")]
        public async Task<IActionResult> InsertEmpPermissionAsync(EmpPermission permission) {
            var result = await _repository.InsertEmpPermissionAsync(permission);

            return Ok(result);
        }

        [HttpPut]
        [Route("Origin/Update")]
        public async Task<IActionResult> UpdateOriginPermissionAsync(OriginPermission permission) {
            var result = await _repository.UpdateOriginPermissionAsync(permission);

            return Ok(result);
        }

        [HttpPut]
        [Route("Department/Update")]
        public async Task<IActionResult> UpdateDeptPermissionAsync(DeptPermission permission) {
            var result = await _repository.UpdateDeptPermissionAsync(permission);

            return Ok(result);
        }

        [HttpPut]
        [Route("Employee/Update")]
        public async Task<IActionResult> UpdateEmpPermissionAsync(EmpPermission permission) {
            var result = await _repository.UpdateEmpPermissionAsync(permission);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Employee/Delete/{id}")]
        public async Task<IActionResult> DeleteEmpPermissionAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteEmpPermissionAsync(id);
            return Ok(result);
        }

    }
}
