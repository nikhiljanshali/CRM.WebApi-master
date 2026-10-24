using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Logistics {
    public class LogisticSearchCriteria {
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public LogisticSearchCriteria() {}
    }
}
