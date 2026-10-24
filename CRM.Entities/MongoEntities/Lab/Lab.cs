using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Lab;
using CRM.Entities.MongoEntities.Organization.DNorm;
namespace CRM.Entities.MongoEntities.LabCollection {
    public class Lab : Document {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string RegistrationNo { get; set; }
        public string IncomeTaxNo { get; set; }
        public string TaxNo { get; set; }
        public Address Address { get; set; }
        public string ExcFormat { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public LabConfig LabConfig { get; set; }
      
        public Lab() {
            Address = new Address();
            LabConfig = new LabConfig();
        }
    }
}
