using CRM.Entities.MongoEntities.LogisticCollection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Logistics {
    public class LogisticResponse {
        public List<Logistic> Logistics;
        public long TotalCount;

        public LogisticResponse() {
            Logistics = new List<Logistic>();
        }
    }
}
