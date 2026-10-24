using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Configuration.Base;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.Configuration {
    public abstract class PermissionGroup : Document {
        public IList<Permission> Menus { get; set; }
        public IList<Permission> NavGroups { get; set; }
        public IList<Permission> NavItems { get; set; }
        public IList<Permission> ReportGroups { get; set; }
        public IList<Permission> ReportItems { get; set; }
        public IList<Permission> Actions { get; set; }
        public IList<GridConfiguration> GridConfigs { get; set; }

        public PermissionGroup() {
            Menus = new List<Permission>();
            NavGroups = new List<Permission>();
            NavItems = new List<Permission>();
            ReportGroups = new List<Permission>();
            ReportItems = new List<Permission>();
            Actions = new List<Permission>();
            GridConfigs = new List<GridConfiguration>();
        }

    }
}