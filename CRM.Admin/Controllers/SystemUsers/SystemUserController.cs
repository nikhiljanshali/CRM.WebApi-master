using CRM.Business.SystemUsers;
using CRM.DAL.SystemUserRepositories;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRM.Entities.MongoEntities.SystemUserNameSpace;

namespace CRM.Admin.Controllers.SystemUsers {

    [Route("api/SystemUser")]
    [ApiController]
    [Authorize]
    public class SystemUserController : ControllerBase {
        private readonly ISystemUserRepository _repository;

        public SystemUserController(ISystemUserRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetSystemUserAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.GetSystemUserAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetSystemUsersAsync() {
            var result = await _repository.GetSystemUsersAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll/{isActive}")]
        public async Task<IActionResult> GetSystemUsersAsync(bool isActive) {
            var result = await _repository.GetSystemUsersAsync(isActive);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll/{skip}/{take}")]
        public async Task<IActionResult> GetSystemUsersAsync(int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetSystemUsersAsync(skip, take);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetSystemUserAsync([FromBody] SystemUserSearchCriteria systemUserCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetSystemUsersAsync(systemUserCriteria, skip, take);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetByOrigin/{origin}")]
        public async Task<IActionResult> GetSystemUsersByOrigin(string origin) {
            if (string.IsNullOrEmpty(origin))
                return BadRequest("Origin is required!");

            var result = await _repository.GetSystemUsersbyOriginAsync(origin);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        [DbLogger(LogDataType.Object, "SystemUser", "SystemUser Inserted")]
        public async Task<IActionResult> InsertSystemUserAsync([FromBody] SystemUser systemUser) {
            var result = await _repository.InsertSystemUserAsync(systemUser);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        [DbLogger(LogDataType.Object, "SystemUser", "SystemUser Updated")]
        public async Task<IActionResult> UpdateSystemUserAsync([FromBody] SystemUser systemUser) {
            var result = await _repository.UpdateSystemUserAsync(systemUser);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [DbLogger(LogDataType.Param, "id", "SystemUser Deleted")]
        public async Task<IActionResult> DeleteSystemUserAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteSystemUserAsync(id);
            return Ok(result);
        }

    }
}