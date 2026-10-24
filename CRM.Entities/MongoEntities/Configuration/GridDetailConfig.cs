using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Common {
    public class GridDetailConfig {
        public string PropertyName { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public int SortOrder { get; set; }
        public bool IsSelected { get; set; }

        public GridDetailConfig() { }
    }
}
