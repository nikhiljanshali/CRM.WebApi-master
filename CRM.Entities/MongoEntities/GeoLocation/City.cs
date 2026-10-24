using CRM.Core.MongoCore.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace CRM.Entities.MongoEntities.GeoLocation {
    public class City : Document {

        [BsonElement("id")]
        public int Ident { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("state_id")]
        public int State_id { get; set; }

        [BsonElement("state_code")]
        public string State_code { get; set; }

        [BsonElement("country_id")]
        public int Country_id { get; set; }

        [BsonElement("country_code")]
        public string Country_Code { get; set; }

        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        public string Longitude { get; set; }

        public City() { }
    }
}