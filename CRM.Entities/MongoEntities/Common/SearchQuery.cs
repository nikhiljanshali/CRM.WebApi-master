using System;

namespace CRM.Entities.MongoEntities.Common {
    public class SearchQuery {
        public string Name { get; set; }
        public string Query { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public SearchQuery() { }

    }
}