using CRM.Business.Common;
using CRM.Core.MongoCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Configuration {
    public class GridConfig : Document {
        public string EmpID { get; set; }
        public string PageName { get; set; }
        public string GridName { get; set; }
        public IList<GridDetailConfig> GridDetail { get; set; }

        public GridConfig() { }
    }
}
