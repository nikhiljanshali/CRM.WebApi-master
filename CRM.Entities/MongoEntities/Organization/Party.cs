using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Party {
    public class Party : Document {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required!")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Party Code is required!")]
        [DataType(DataType.Text)]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact Person is required!")]
        [DataType(DataType.Text)]
        public string ContactPerson { get; set; }

        public Address Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string IncomeTaxNo { get; set; }
        public string TaxNo { get; set; }
        public string LineOfBusiness { get; set; }
        public bool IsActive { get; set; }

        public Party() {
            Address = new Address();
        }
    }
}