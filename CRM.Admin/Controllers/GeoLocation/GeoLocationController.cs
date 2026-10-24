using CRM.DAL.GeoLocationRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlowStar.WebApi.Controllers.GeoLocation {
    [Route("api/GeoLocation")]
    [ApiController]
    //[Authorize]
    public class GeoLocationController : ControllerBase {
        private IGeoLocationRepository _repository;

        public GeoLocationController(IGeoLocationRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetCountry/{ident}")]
        public async Task<IActionResult> GetCountryAsync(int ident) {
            if (ident <= 0)
                return BadRequest("Ident must be greater then zero!");

            var result = await _repository.GetCountryAsync(ident);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCountry/{code}")]
        public async Task<IActionResult> GetCountryAsync(string code) {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Code is required!");

            var result = await _repository.GetCountryAsync(code);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountriesAsync() {
            var result = await _repository.GetCountriesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCountries/{region}")]
        public async Task<IActionResult> GetCountriesAsync(string region) {
            if (string.IsNullOrEmpty(region))
                return BadRequest("Region is required!");

            var result = await _repository.GetCountriesAsync(region);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetState/{ident}")]
        public async Task<IActionResult> GetStateAsync(int ident) {
            if (ident <= 0)
                return BadRequest("Ident must be greater then zero!");

            var result = await _repository.GetState(ident);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetState/{code}")]
        public async Task<IActionResult> GetStateAsync(string code) {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Code is required!");

            var result = await _repository.GetState(code);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetStates")]
        public async Task<IActionResult> GetStatesAsync() {
            var result = await _repository.GetStates();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetStates/{country}")]
        public async Task<IActionResult> GetStatesAsync(string country) {
            if (string.IsNullOrEmpty(country))
                return BadRequest("Country is required!");

            var result = await _repository.GetStates(country);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCity/{ident}")]
        public async Task<IActionResult> GetCityAsync(int ident) {
            if (ident <= 0)
                return BadRequest("Ident must be greater then zero!");

            var result = await _repository.GetCity(ident);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCities")]
        public async Task<IActionResult> GetCities() {
            var result = await _repository.GetCities();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCities/{state}/{country}")]
        public async Task<IActionResult> GetCitiesAsync(string state, string country) {
            if (string.IsNullOrEmpty(state) || string.IsNullOrEmpty(country))
                return BadRequest("Country & State is required!");

            var result = await _repository.GetCities(state,country);
            return Ok(result);
        }
    }
}
