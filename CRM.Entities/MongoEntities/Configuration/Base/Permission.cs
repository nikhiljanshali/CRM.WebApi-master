namespace CRM.Entities.MongoEntities.Configuration {
    public class Permission {

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Identifier { get; set; }
        public bool IsManagerOnly { get; set; }

        public Permission() { }
        public Permission(string name, string displayName, string identifier) {
            this.Name = name;
            this.DisplayName = displayName;
            this.Identifier = identifier;
        }

    }
}