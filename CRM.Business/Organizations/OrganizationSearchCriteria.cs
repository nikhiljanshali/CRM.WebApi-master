using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Organizations {
    public class OrganizationSearchCriteria {
        public string Name { get; set; }
        public string OrganizationType { get; set; }
        public string BusinessType { get; set; }
        public string RegistrationNo { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public OrganizationSearchCriteria() {
        }
    }
}