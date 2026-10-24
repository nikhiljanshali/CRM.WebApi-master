using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Promotional.DNorm {
    public class SchemeDetail {
        public string Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Badge name is required!")]
        public string BadgeName { get; set; }

        [Required]
        public decimal? From { get; set; }

        [Required]
        public decimal? To { get; set; }

        [Required]
        public int Days { get; set; }

        public decimal? Online { get; set; }

        public decimal? Offline { get; set; }

        public SchemeDetail() { }

    }
}