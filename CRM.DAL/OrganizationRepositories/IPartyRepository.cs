using CRM.Business.PartyNames;
using CRM.Entities.MongoEntities.Party;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public interface IPartyRepository {
        Task<IList<Party>> GetPartyAsync();
        Task<PartyResponse> GetPartyAsync(PartySearchCriteria PartyCriteria, int skip, int take);
        Task<Party> GetPartyAsync(string id);
        Task<string> InsertPartyAsync(Party party);
        Task<bool> UpdatePartyAsync(Party party);
        Task<long> DeletePartyAsync(string partyId);
    }
}