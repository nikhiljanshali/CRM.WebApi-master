using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Configuration {
    public class MasterConfigType {
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public MasterConfigType() {
        }
    }
}