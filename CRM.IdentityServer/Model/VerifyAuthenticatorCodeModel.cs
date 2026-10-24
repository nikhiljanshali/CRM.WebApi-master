using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class VerifyAuthenticatorCodeModel {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Code is required!")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
