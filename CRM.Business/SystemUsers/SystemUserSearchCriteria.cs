using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.SystemUsers {
    public class SystemUserSearchCriteria {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Origin { get; set; }
        public bool? IsActive { get; set; }

        public SystemUserSearchCriteria() { }
    }
}
