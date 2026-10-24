using CRM.Business.PartyNames;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Party;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public class PartyRepository : IPartyRepository {
        private readonly IMongoRepository _repository;

        public PartyRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<Party> GetPartyAsync(string id) {
            return await _repository.GetOneAsync<Party>(c => c.Id == id);
        }

        public async Task<IList<Party>> GetPartyAsync() {
            return await _repository.GetAllAsync<Party>(c => c.Id != null);
        }

        public async Task<PartyResponse> GetPartyAsync(PartySearchCriteria PartyCriteria, int skip, int take) {
            var prtResponse = new PartyResponse();
            Expression<Func<Party, bool>> filter = c => (string.IsNullOrEmpty(PartyCriteria.Name) || c.Name.ToLower().Contains(PartyCriteria.Name.ToLower()))
             && (string.IsNullOrEmpty(PartyCriteria.Code) || c.Code.ToLower().Contains(PartyCriteria.Code.ToLower()))
             && (string.IsNullOrEmpty(PartyCriteria.ContactPerson) || c.ContactPerson.ToLower().Contains(PartyCriteria.ContactPerson.ToLower()))
             && (string.IsNullOrEmpty(PartyCriteria.Email) || c.Email.ToLower().Contains(PartyCriteria.Email.ToLower()))
             && (string.IsNullOrEmpty(PartyCriteria.MobileNo) || c.MobileNo.ToLower().Contains(PartyCriteria.MobileNo.ToLower()))
             && (string.IsNullOrEmpty(PartyCriteria.PhoneNo) || c.PhoneNo.ToLower().Contains(PartyCriteria.PhoneNo.ToLower()))
             && c.Id != null;

            prtResponse.Party = await _repository.GetPaginatedAsync<Party>(filter, skip, take);
            prtResponse.TotalCount = await _repository.CountAsync<Party>(filter);

            return prtResponse;
        }

        public async Task<string> InsertPartyAsync(Party party) {
            await _repository.AddOneAsync(party);

            return party.Id;
        }

        public async Task<bool> UpdatePartyAsync(Party party) {
            var result = await _repository.UpdateOneAsync<Party>(party);

            return result;
        }

        public async Task<long> DeletePartyAsync(string id) {
            return await _repository.DeleteOneAsync<Party>(c => c.Id == id);
        }

    }
}
