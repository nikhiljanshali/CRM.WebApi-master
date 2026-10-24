using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Common {
    public class UserCredential {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Organization { get; set; }
        public string OrganizationId { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string Branch { get; set; }
        public string Origin { get; set; }
        public string Code { get; set; }
        public bool IsManager { get; set; }
        public bool IsAvailable { get; set; }
        public string DeviceRFIDUrl { get; set; }

        public UserCredential() { }
    }
}
