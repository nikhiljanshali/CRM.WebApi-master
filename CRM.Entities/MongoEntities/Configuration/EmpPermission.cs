using CRM.Entities.MongoEntities.Configuration.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Configuration {
    public class EmpPermission : PermissionGroup{
        [Required(AllowEmptyStrings =false,ErrorMessage ="User id is required!")]
        public string EmployeeId { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="User name is required!")]
        public string EmployeeName { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Origin type is required!")]
        public string Origin { get; set; }

        public IList<IssueRight> IssueRights { get; set; }

        public EmpPermission() {
            IssueRights = new List<IssueRight>();
        }

    }
}