using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Model {
    public class RemoveUserModel {
        [Required(AllowEmptyStrings =false,ErrorMessage ="User id is required!")]
        public string UserId { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Email is required!")]
        public string Email { get; set; }

        public RemoveUserModel() { }

    }
}
