using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.MongoEntities.Configuration.DNorm {
    public class MasterDNorm {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "name is required!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Display name is required!")]
        public string DisplayName { get; set; }
        
        public string Priority { get; set; }
        
        public string Type { get; set; }
        
        public IList<string> OptionalWords { get; set; }

        public MasterDNorm() {
            OptionalWords = new List<string>();
        }
    }
}
