using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class AddPhoneNumberModel {
        [Required(AllowEmptyStrings =false,ErrorMessage ="User id is required!")]
        public string UserId { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public AddPhoneNumberModel() { }
    }
}
