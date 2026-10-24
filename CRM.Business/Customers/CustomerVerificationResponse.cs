using CRM.Entities.MongoEntities.Registration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Customers {
    public class CustomerVerificationResponse {
        public List<RegisterCustomer> Customers;
        public long TotalCount;

        public CustomerVerificationResponse() {
            Customers = new List<RegisterCustomer>();
        }
    }
}