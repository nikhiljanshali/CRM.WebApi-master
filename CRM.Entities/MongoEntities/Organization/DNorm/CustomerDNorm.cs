using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class CustomerDNorm {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Id is required!")]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is required!")]        
        public string Mobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public CustomerDNorm() { }
        public CustomerDNorm(string id, string name, string mobile, string email) {
            this.Id = id;
            this.Name = name;
            this.Mobile = mobile;
            this.Email = email;
        }
    }
}