using CRM.Core.FrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRM.Core.MongoCore.Model {

    /// <summary>
    /// Class for store historical document.
    /// </summary>
    /// <typeparam name="T">Old document.</typeparam>
    public class DocumentX<TDocument> where TDocument : IEntity {

        /// <summary>
        /// The Id of the document
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The historical document
        /// </summary>
        [BsonElement]
        public TDocument XDocument { get; set; }
    }
}
