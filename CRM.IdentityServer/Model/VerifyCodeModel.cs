using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class VerifyCodeModel {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Provider is required!")]
        public string Provider { get; set; }

        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public VerifyCodeModel() { }
    }
}
