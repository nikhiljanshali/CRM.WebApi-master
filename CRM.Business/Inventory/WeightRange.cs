using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Inventory {
    public class WeightRange {
        public decimal? MinWeight { get; set; }
        public decimal? MaxWeight { get; set; }

        public WeightRange() { }
    }
}
