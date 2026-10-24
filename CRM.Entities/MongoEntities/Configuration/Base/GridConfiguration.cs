using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Configuration.Base {
    public class GridConfiguration {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Grid name is required!")]
        public string GridName { get; set; }

        public IList<ColumnConfig> Configs { get; set; }

        public GridConfiguration() {
            Configs = new List<ColumnConfig>();
        }

    }
}
