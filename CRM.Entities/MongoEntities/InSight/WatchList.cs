using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.InSight {
    public class WatchList : Document {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Stone id is required!")]
        public string StoneId { get; set; }

        public CustomerDNorm Customer { get; set; }

        [Required]
        public decimal Rap { get; set; }

        [Required]
        public decimal Discount { get; set; }

        public WatchList() {
            Customer = new CustomerDNorm();
        }
    }
}