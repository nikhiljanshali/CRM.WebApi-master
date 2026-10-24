using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Lab {
    public class LabConfig {
        public string LabAPI { get; set; }
        public IList<LabLabour> LabLabours { get; set; }
        
        public LabConfig() {
            LabLabours = new List<LabLabour>();
        }
    }
}
