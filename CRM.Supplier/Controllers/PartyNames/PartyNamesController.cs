using CRM.DAL.ConfigRepositories;
using CRM.Logging.Attributes;
using CRM.Logging.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRM.Business.PartyNames;
using CRM.Entities.MongoEntities.Party;

namespace CRM.Supplier.Controllers.PartyNames {
    [Route("api/PartyNames")]
    [ApiController]
    [Authorize]
    public class PartyNamesController : ControllerBase {
        private readonly IPartyRepository _repository;

        public PartyNamesController(IPartyRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetPartyAsync(string id) {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id is required!");
            var result = await _repository.GetPartyAsync(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetPartyAsync() {
            var result = await _repository.GetPartyAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("GetPaginated/{skip}/{take}")]
        public async Task<IActionResult> GetPartyAsync([FromBody] PartySearchCriteria PartyCriteria, int skip, int take) {
            if (take <= 0)
                return BadRequest("Take must be greater then zero!");

            var result = await _repository.GetPartyAsync(PartyCriteria, skip, take);
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertParty")]
        [DbLogger(LogDataType.Param, "Party", "Party Inserted")]
        public async Task<IActionResult> InsertPartyAsync([FromBody] Party party) {
            var result = await _repository.InsertPartyAsync(party);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateParty")]
        [DbLogger(LogDataType.Param, "Party", "Party Updated")]
        public async Task<IActionResult> UpdatePartyAsync([FromBody] Party party) {
            if (string.IsNullOrEmpty(party.Id))
                return BadRequest("Party id is required!");

            var result = await _repository.UpdatePartyAsync(party);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteParty/{partyId}")]
        [DbLogger(LogDataType.Param, "partyId", "Party Deleted")]
        public async Task<IActionResult> DeletePartyAsync(string partyId) {
            if (string.IsNullOrEmpty(partyId))
                return BadRequest("party id is required!");

            var result = await _repository.DeletePartyAsync(partyId);
            return Ok(result);
        }

    }
}