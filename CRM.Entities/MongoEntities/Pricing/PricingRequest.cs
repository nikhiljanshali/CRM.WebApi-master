using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Pricing {
    public class PricingRequest : TempPricing {
        public string Status { get; set; }

        public PricingRequest() { }
    }
}
