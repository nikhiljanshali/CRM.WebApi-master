using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Employees {
    public class EmployeeSearchCriteria {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationId { get; set; }
        public string Branch { get; set; }
        public string DepartmentId { get; set; }
        public string Origin { get; set; }
        public bool? IsActive { get; set; }

        public EmployeeSearchCriteria() { }
    }
}