using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class VerifyPhoneNumberModel {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public VerifyPhoneNumberModel() { }
    }
}
