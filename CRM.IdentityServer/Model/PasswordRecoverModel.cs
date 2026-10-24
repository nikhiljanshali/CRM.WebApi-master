using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Modal {
    public class PasswordRecoverModel {
        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required!")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "OTP is required!")]
        public string OTP { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public PasswordRecoverModel() { }
    }
}
