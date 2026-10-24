using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.Organization {
    public class CustomerPreference : Document {
        public CustomerDNorm Customer { get; set; }
        public ExportSetup ExportInfo { get; set; }
        public IList<SearchQuery> PrefrredStones { get; set; }
        public IList<SearchQuery> SavedSearches { get; set; }
        public IList<SearchQuery> StoneAlerts { get; set; }

        public CustomerPreference() {
            Customer = new CustomerDNorm();
            ExportInfo = new ExportSetup();
            PrefrredStones = new List<SearchQuery>();
            SavedSearches = new List<SearchQuery>();
            StoneAlerts = new List<SearchQuery>();
        }

    }
}