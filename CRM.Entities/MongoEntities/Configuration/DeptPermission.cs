using CRM.Entities.MongoEntities.Configuration.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Configuration {
    public class DeptPermission : PermissionGroup{
        [Required(AllowEmptyStrings =false, ErrorMessage ="Department id is required!")]
        public string DepartmentId { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Department name is required!")]
        public string Department { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Origin is required!")]
        public string Origin { get; set; }

        public IList<IssueRight> IssueRights { get; set; }

        public DeptPermission() {
            IssueRights = new List<IssueRight>();
        }

    }
}
