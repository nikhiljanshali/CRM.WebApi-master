using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.GeoLocation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.GeoLocationRepositories {
    public class GeoLocationRepository : IGeoLocationRepository {
        private readonly IMongoRepository _repository;

        public GeoLocationRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<Country> GetCountryAsync(int ident) {
            return await _repository.GetOneAsync<Country>(c => c.Ident == ident);
        }
        public async Task<Country> GetCountryAsync(string code) {
            return await _repository.GetOneAsync<Country>(c => c.Iso2 == code || c.Iso3 == code);
        }
        public async Task<IList<Country>> GetCountriesAsync() {
            return await _repository.GetAllAsync<Country>(c => c.Id != null);
        }
        public async Task<IList<Country>> GetCountriesAsync(string region) {
            return await _repository.GetAllAsync<Country>(c => c.Region == region);
        }

        public async Task<State> GetState(int ident) {
            return await _repository.GetOneAsync<State>(c => c.Ident == ident);
        }
        public async Task<State> GetState(string code) {
            return await _repository.GetOneAsync<State>(c => c.State_Code == code);
        }
        public async Task<IList<State>> GetStates() {
            return await _repository.GetAllAsync<State>(c => c.Id != null);
        }
        public async Task<IList<State>> GetStates(string country) {
            return await _repository.GetAllAsync<State>(c => c.Country_code == country);
        }

        public async Task<City> GetCity(int ident) {
            return await _repository.GetOneAsync<City>(c => c.Ident == ident);
        }
        public async Task<IList<City>> GetCities() {
            return await _repository.GetAllAsync<City>(c => c.Id != null);
        }
        public async Task<IList<City>> GetCities(string state,string country) {
            return await _repository.GetAllAsync<City>(c => c.State_code == state && c.Country_Code == country);
        }
    }
}
