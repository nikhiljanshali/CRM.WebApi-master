using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.PartyNames {
    public class PartySearchCriteria {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public PartySearchCriteria() {
        }
    }
}
