using CRM.Business.Inventory;
using CRM.Entities.MongoEntities.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Pricing {
    public class PriceRequestApiModel {
        public IList<EmployeeCriteria> Slots { get; set; }
        public InventorySearchCriteria InvFilter { get; set; }
        public string Type { get; set; }
        public string EmployeeId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public PriceRequestApiModel() {
            Slots = new List<EmployeeCriteria>();
        }

    }
}
