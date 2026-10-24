using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Modal {
    public class PasswordChangeModel {

        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required!")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Old password is required!")]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public PasswordChangeModel() { }
    }
}
