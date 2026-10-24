using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class ForgotPasswordModel {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Email is required!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
