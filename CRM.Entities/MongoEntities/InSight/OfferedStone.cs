using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.InSight {
    public class OfferedStone : Document {
        public CustomerDNorm Customer { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Stone id is required!")]
        public string StoneId { get; set; }

        [Required]
        public decimal Rap { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal OfferedDisc { get; set; }

        [Required]
        public int Days { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }

        public OfferedStone() {
            Customer = new CustomerDNorm();
        }

    }
}