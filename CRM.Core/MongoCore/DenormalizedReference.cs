using CRM.Core.FrameworkCore;
using MongoDB.Bson.Serialization.Attributes;

namespace CRM.Core.MongoCore {
    /// <summary>
    /// this is a special custom type and used throughout the framework for some very general purposes.
    /// </summary>
    public class DenormalizedReference : IDenormalizedReference {

        [BsonElement("id")]
        public string DenormalizedId { get; set; }

        [BsonElement("nm")]
        public string DenormalizedName { get; set; }

    }
}
