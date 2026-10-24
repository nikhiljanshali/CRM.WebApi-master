using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Configuration.DNorm {
    public class CutDetailDNorm {
        public string Id { get; set; }
        public string CPS { get; set; }
        public string Shape { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }

        public CutDetailDNorm() { }
    }
}
