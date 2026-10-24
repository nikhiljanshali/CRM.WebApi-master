using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.MongoEntities.Inventory {
    public class StoneOrgDNorm {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Organization Id is required!")]
        public string OrgId { get; set; }

        public string OrgName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Department Id is required!")]
        public string DeptId { get; set; }

        public string DeptName { get; set; }

        public string BranchName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public StoneOrgDNorm() { }
    }
}
