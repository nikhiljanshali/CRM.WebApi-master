using System;
using System.Collections.Generic;
using System.Text;
using CRM.Entities.MongoEntities.Organization;

namespace CRM.Business.Employees {
    public class EmployeeResponse {
        public List<Employee> Employees;
        public long TotalCount;

        public EmployeeResponse() {
            Employees = new List<Employee>();
        }
    }
}