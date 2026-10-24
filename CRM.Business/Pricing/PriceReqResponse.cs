using CRM.Entities.MongoEntities.Pricing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Pricing {
    public class PriceReqResponse {
        public List<PricingRequest> PriceRequest;
        public long TotalCount;
        public decimal? TotalWeight;
        public decimal? TotalNetAmount;

        public PriceReqResponse() {
            PriceRequest = new List<PricingRequest>();
        }
    }
}
