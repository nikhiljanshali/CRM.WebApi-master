using CRM.Entities.MongoEntities.LabCollection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Labs {
    public class LabResponse {
        public List<Lab> Labs;
        public long TotalCount;

        public LabResponse() {
            Labs = new List<Lab>();
        }
    }
}