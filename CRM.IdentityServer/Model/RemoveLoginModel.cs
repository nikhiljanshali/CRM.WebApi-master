using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class RemoveLoginModel {
        [Required(AllowEmptyStrings =false, ErrorMessage ="User id is required!")]
        public string UserId { get; set; }

        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        public RemoveLoginModel() { }
    }
}
