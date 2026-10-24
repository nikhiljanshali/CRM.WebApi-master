using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.InSight {
    public class PreOrderStone : Document {
        public CustomerDNorm Customer { get; set; }
        public IList<SearchQuery> Stones { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public PreOrderStone() {
            Customer = new CustomerDNorm();
            Stones = new List<SearchQuery>();
        }
    }
}