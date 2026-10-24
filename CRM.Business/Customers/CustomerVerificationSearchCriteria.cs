using CRM.Entities.MongoEntities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Customers {
    public class CustomerVerificationSearchCriteria {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }

        public CustomerVerificationSearchCriteria() {
            Status = StatusType.InProgress.ToString();
        }
    }
}