using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CRM.Core.MongoCore.Model {

    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public class Document : IDocument {

        /// <summary>
        /// The document constructor
        /// </summary>
        public Document() {
            CreatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// The Id of the document
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The version of the schema of the document
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The user who created this document
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// The datetime in UTC at which the document was added.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The user who last updated this document
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// The datetime in UTC at which the document was updated.
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
    }
}
