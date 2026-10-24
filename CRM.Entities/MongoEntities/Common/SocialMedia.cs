namespace CRM.Entities.MongoEntities.Common {
    /// <summary>
    /// Social media profiles
    /// </summary>
    public class SocialMedia {

        /// <summary>
        /// Social media provider
        /// </summary>
        public SocialMediaProvider Provider { get; set; }
               
        /// <summary>
        /// Profile name if media type is identity name based
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// phone number if media type is on registred mobile no.
        /// </summary>
        public string MobileNumber { get; set; }

        public SocialMedia() {
            Provider = new SocialMediaProvider();
        }

    }
}