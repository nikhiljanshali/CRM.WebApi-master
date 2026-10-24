using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Organization {
    public class Organization : Document {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Name is required!")]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings =false, ErrorMessage ="Origin is required!")]
        public string Origin { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Organization type is required!")]
        public string OrganizationType { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Business type is required!")]
        public string BusinessType { get; set; }
        public string RegistrationNo { get; set; }
        public string IncomeTaxNo { get; set; }
        public string TaxNo { get; set; }
        public string Membership { get; set; }
        public Address Address { get; set; }
        public string Website { get; set; }
        public CreditLimit CreditLimit { get; set; }
        public IList<Branch> Branches { get; set; }
        public IList<Department> Departments { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Person is required!")]
        public string Person { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Email is required!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Mobile is required!")]
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }

        public Organization() {
            Address = new Address();
            CreditLimit = new CreditLimit();
            Branches = new List<Branch>();
            Departments = new List<Department>();
        }

    }
}