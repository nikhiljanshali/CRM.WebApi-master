namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class OrganizationDNorm {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Person { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }

        public OrganizationDNorm() { }
        public OrganizationDNorm(string id, string name, string person, string phoneNo) {
            this.Id = id;
            this.Name = name;
            this.Person = person;
            this.PhoneNo = phoneNo;
        }

    }
}