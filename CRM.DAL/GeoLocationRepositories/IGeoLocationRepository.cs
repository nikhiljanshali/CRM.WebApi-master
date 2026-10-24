using CRM.Entities.MongoEntities.GeoLocation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.GeoLocationRepositories {
    public interface IGeoLocationRepository {
        Task<Country> GetCountryAsync(int ident);
        Task<Country> GetCountryAsync(string code);
        Task<IList<Country>> GetCountriesAsync();
        Task<IList<Country>> GetCountriesAsync(string region);

        Task<State> GetState(int ident);
        Task<State> GetState(string code);
        Task<IList<State>> GetStates();
        Task<IList<State>> GetStates(string country);

        Task<City> GetCity(int ident);
        Task<IList<City>> GetCities();
        Task<IList<City>> GetCities(string state, string country);
    }
}
