using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class RegisterModel {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Ident is required!")]
        public string Ident { get; set; }

        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Origin is required!")]
        public string Origin { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is required!")]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required!")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public RegisterModel() { }
    }
}
