using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class EmployeeDNorm {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required!")]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        public string Department { get; set; }
        public string Organization { get; set; }
        public string Branch { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is required!")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public EmployeeDNorm() { }
        public EmployeeDNorm(string id, string name, string dept, string org, string branch, string mobile, string email) {
            this.Id = id;
            this.Name = name;
            this.Department = dept;
            this.Organization = org;
            this.Branch = branch;
            this.Mobile = mobile;
            this.Email = email;
        }
    }
}
