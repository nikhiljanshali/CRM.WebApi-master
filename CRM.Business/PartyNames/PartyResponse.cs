using System.Collections.Generic;
using CRM.Entities.MongoEntities.Party;

namespace CRM.Business.PartyNames {
    public class PartyResponse {
        public List<Party> Party;
        public long TotalCount;
        public PartyResponse() {
            Party = new List<Party>();
        }
    }
}