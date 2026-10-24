using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Configuration {
    public class OriginPermission : PermissionGroup {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Origin is required!")]
        public string Origin { get; set; }

        public OriginPermission() {}

    }
}