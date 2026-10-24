using CRM.Core.MongoCore.Model;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Common {
    public class SocialMediaProvider : Document {

        /// <summary>
        /// Company name who own platform like facebook, whatsapp and instagram
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Social media type
        /// </summary>
        [Required(ErrorMessage = "Social media type is required!")]
        public SocialMediaType MediaType { get; set; }

        public SocialMediaProvider() { }

    }
}