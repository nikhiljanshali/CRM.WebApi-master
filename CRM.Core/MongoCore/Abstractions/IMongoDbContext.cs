using MongoDB.Driver;

namespace CRM.Core.MongoCore {

    /// <summary>
    /// This is the interface of the IMongoDbContext which is managed by the <see cref="ReadOnlyMongoRepository"/>.
    /// </summary>
    public interface IMongoDbContext {
        /// <summary>
        /// The IMongoClient from the official MongoDb driver
        /// </summary>
        IMongoClient Client { get; }

        /// <summary>
        /// The IMongoDatabase from the official Mongodb driver
        /// </summary>
        IMongoDatabase Database { get; }

        /// <summary>
        /// Returns a collection for a document type that has a partition key.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="partitionKey">The value of the partition key.</param>
        IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null);

        /// <summary>
        /// Drops a collection having a partitionkey, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="partitionKey">Partition Key</param>
        void DropCollectionByPartitionKey<TDocument>(string partitionKey = null);

        /// <summary>
        /// Drops the collection.This same concept is there in other databases such as arango, raven etc.
        /// Whereas coluchbase and couchdb doesn't have explicit collections, but the same concept of clearing all documents of particular types applies good.
        /// </summary>
        /// <param name="collectionName"></param>
        void DropCollection(string collectionName);

        /// <summary>
        /// drops the database
        /// </summary>
        void DropDatabase();        

    }
}
