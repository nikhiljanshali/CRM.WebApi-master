using CRM.Core.MongoCore.Model;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.GeoLocation {
    [BsonIgnoreExtraElements]
    public class Country : Document {

        [BsonElement("id")]
        public int Ident { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("iso3")]
        public string Iso3 { get; set; }

        [BsonElement("iso2")]
        public string Iso2 { get; set; }

        [BsonElement("phone_code")]
        public string Phone_code { get; set; }

        [BsonElement("capital")]
        public string Capital { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }

        [BsonElement("currency_symbol")]
        public string Currency_symbol { get; set; }

        [BsonElement("tld")]
        public string Tld { get; set; }

        [BsonElement("native")]
        public string Native { get; set; }

        [BsonElement("region")]
        public string Region { get; set; }

        [BsonElement("subregion")]
        public string Subregion { get; set; }

        [BsonElement("timeZones")]
        public IList<TimeZone> TimeZones { get; set; }

        [BsonElement("translations")]
        public Translations Translations { get; set; }

        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        public string Longitude { get; set; }

        [BsonElement("emoji")]
        public string Emoji { get; set; }

        [BsonElement("emojiU")]
        public string EmojiU { get; set; }

        public Country() {
            TimeZones = new List<TimeZone>();
            Translations = new Translations();
        }

    }
}