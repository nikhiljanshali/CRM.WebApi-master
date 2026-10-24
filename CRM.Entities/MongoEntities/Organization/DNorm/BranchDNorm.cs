using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class BranchDNorm {
        public string Name { get; set; }
        public string Person { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string DeviceRFIDUrl { get; set; }

        public BranchDNorm() { }
    }
}