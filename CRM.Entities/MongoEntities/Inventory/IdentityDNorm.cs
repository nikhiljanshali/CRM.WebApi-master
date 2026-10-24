using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.MongoEntities.Inventory {
    public class IdentityDNorm {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required!")]
        public string Id { get; set; }

        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required!")]
        public string Type { get; set; }

        public IdentityDNorm() { }
    }
}
