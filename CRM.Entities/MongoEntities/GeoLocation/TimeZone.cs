using MongoDB.Bson.Serialization.Attributes;

namespace CRM.Entities.MongoEntities.GeoLocation {
    public class TimeZone {

        [BsonElement("abbreviation")]
        public string Abbreviation { get; set; }

        [BsonElement("gmtOffset")]
        public long GmtOffset { get; set; }

        [BsonElement("gmtOffsetName")]
        public string GmtOffsetName { get; set; }

        [BsonElement("tzName")]
        public string TzName { get; set; }

        [BsonElement("zoneName")]
        public string ZoneName { get; set; }

        public TimeZone() { }
    }
}