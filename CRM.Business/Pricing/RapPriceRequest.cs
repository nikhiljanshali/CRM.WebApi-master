using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Pricing {
    public class RapPriceRequest {
        public string Shape;
        public decimal Weight;
        public string Color;
        public string Clarity;

        public RapPriceRequest() { }
    }
}
