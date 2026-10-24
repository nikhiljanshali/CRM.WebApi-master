using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class Branch {
        public string Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Name is required!")]
        public string Name { get; set; }
        public string RegistrationNo { get; set; }
        public string IncomeTaxNo { get; set; }
        public Address Address { get; set; }
        public string TaxNo { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Person is required!")]
        public string Person { get; set; }
        
        [Required(AllowEmptyStrings =false,ErrorMessage ="Email is required!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Mobile number is required!")]
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string DeviceRFIDUrl { get; set; }

        public Branch() {
            Address = new Address();
        }

    }
}