using CRM.Core.MongoCore.Abstractions;
using CRM.Core.MongoCore.Model;
using MongoDB.Driver;
using System;

namespace CRM.Core.MongoCore {

    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    /// <typeparam name="TKey">The type of the document Id.</typeparam>
    public abstract partial class BaseMongoRepository<TKey> : ReadOnlyMongoRepository<TKey>, IBaseMongoRepository<TKey>
        where TKey : IEquatable<TKey> {

        /// <summary>
        /// The object lock for thread syncing.
        /// </summary>
        private readonly object _initLock = new object();

        /// <summary>
        /// The constructor taking a connection string.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        protected BaseMongoRepository(string connectionString) : base(connectionString) {
        }

        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected BaseMongoRepository(string connectionString, string databaseName)
            : base(connectionString, databaseName) {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDbContext"/>.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext"/></param>
        protected BaseMongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext) {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase"/></param>
        protected BaseMongoRepository(IMongoDatabase mongoDatabase) : this(new MongoDbContext(mongoDatabase)) {
        }

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        protected virtual IMongoCollection<TDocument> HandlePartitioned<TDocument>(string partitionKey)
            where TDocument : IDocument<TKey> {

            if (!string.IsNullOrEmpty(partitionKey)) 
                return GetCollection<TDocument>(partitionKey);
            
            return GetCollection<TDocument>();
        }

        /// <summary>
        /// Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        protected virtual IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null)
            where TDocument : IDocument<TKey> {

            return MongoDbContext.GetCollection<TDocument>(partitionKey);
        }

    }
        
}
