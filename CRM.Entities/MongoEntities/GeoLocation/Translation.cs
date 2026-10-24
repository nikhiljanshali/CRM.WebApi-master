using MongoDB.Bson.Serialization.Attributes;

namespace CRM.Entities.MongoEntities.GeoLocation {

    [BsonIgnoreExtraElements]

    public class Translations {

        [BsonElement("br")]
        public string Br { get; set; }

        [BsonElement("de")]
        public string De { get; set; }
        
        [BsonElement("es")]
        public string Es { get; set; }

        [BsonElement("fa")]
        public string Fa { get; set; }

        [BsonElement("hr")]
        public string Hr { get; set; }

        [BsonElement("it")]
        public string It { get; set; }

        [BsonElement("ja")]
        public string Ja { get; set; }

        [BsonElement("nl")]
        public string Nl { get; set; }

        [BsonElement("pt")]
        public string Pt { get; set; }

        public Translations() { }
    }
}