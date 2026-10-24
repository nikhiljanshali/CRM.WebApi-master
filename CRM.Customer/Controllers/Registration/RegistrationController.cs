using CRM.DAL.RegistrationRepositories;
using CRM.Entities.MongoEntities.Registration;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace recursivediamond.WebApi.Controllers.Registration {
    [Route("api/Register")]
    [ApiController]
    public class RegistrationController : ControllerBase {
        private readonly IRegistrationRepository _repository;

        public RegistrationController(IRegistrationRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetRequests")]
        public async Task<IActionResult> GetRequestAsync() {

            var result = await _repository.GetRegisterCustomersAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetRequests/{status}")]
        public async Task<IActionResult> GetRequestAsync(string status) {
            if (string.IsNullOrEmpty(status))
                return BadRequest("status is required!");

            var result = await _repository.GetRegisterCustomersAsync(status);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetRequests/{take}/{skip}")]
        public async Task<IActionResult> GetRequestAsync(int take, int skip) {
            var result = await _repository.GetRegisterCustomersAsync(skip, take);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetRequests/{status}/{take}/{skip}")]
        public async Task<IActionResult> GetRequestAsync(string status, int take, int skip) {
            var result = await _repository.GetRegisterCustomersAsync(status, skip, take);

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertRequest")]
        [AllowAnonymous]
        [DbLogger(LogDataType.Object,"registration","Registration Insert")]
        public async Task<IActionResult> InsertRequestAsync(RegisterCustomer registration) {
            bool flag = await _repository.ExistAcceptedCustomerAsync(registration);
            if (flag)
                return BadRequest("You have already registered");

            flag = await _repository.ExistInProgressCustomerAsync(registration);
            if (flag)
                return BadRequest("You have already applied for registration, keep in touch with you soon");

            string result = await _repository.InsetRegisterCustomerAsync(registration);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateRequest")]
        [DbLogger(LogDataType.Object, "registration", "Registration Update")]
        public async Task<IActionResult> UpdateRequestAsync(RegisterCustomer registration) {
            var result = await _repository.UpdateRegisterCustomerAsync(registration);

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteRequest/{id}")]
        [DbLogger(LogDataType.Param, "id", "Registration Delete")]
        public async Task<IActionResult> DeleteRequestAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");

            var result = await _repository.DeleteRegisterCustomerAsync(id);
            return Ok(result);
        }

    }
}