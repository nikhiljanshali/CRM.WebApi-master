using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Inventory {
    public class PriceDNorm {
        public decimal? Rap { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PerCarat { get; set; }
    }
}
