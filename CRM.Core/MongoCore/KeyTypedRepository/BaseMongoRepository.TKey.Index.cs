using CRM.Core.MongoCore.Abstractions;
using CRM.Core.MongoCore.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.Core.MongoCore {

    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository<TKey> : IBaseMongoRepository_Index<TKey> {

        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        public async virtual Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey = null)
            where TDocument : IDocument<TKey> {
            var indexCursor = await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes.ListAsync();
            var indexes = await indexCursor.ToListAsync();

            return indexes.Select(e => e["name"].ToString()).ToList();
        }

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public async virtual Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Indexes
                .CreateOneAsync( new CreateIndexModel<TDocument>(
                    Builders<TDocument>.IndexKeys.Text(field),
                    indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions)
                    ));
        }

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public async virtual Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;

            return await collection
                .Indexes
                .CreateOneAsync(
                new CreateIndexModel<TDocument>(indexKey.Ascending(field), createOptions));
        }

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public async virtual Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;

            return await collection
                .Indexes
                .CreateOneAsync(
                new CreateIndexModel<TDocument>(indexKey.Descending(field), createOptions));
        }

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public async virtual Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;

            return await collection
                .Indexes
                .CreateOneAsync(
                new CreateIndexModel<TDocument>(indexKey.Hashed(field), createOptions));
        }

        /// <summary>
        /// Creates a combined text index.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="fields">The fields we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public async virtual Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);

            var listOfDefs = new List<IndexKeysDefinition<TDocument>>();
            foreach (var field in fields) 
                listOfDefs.Add(Builders<TDocument>.IndexKeys.Text(field));
            
            return await collection
                .Indexes
                .CreateOneAsync(
                new CreateIndexModel<TDocument>(Builders<TDocument>.IndexKeys.Combine(listOfDefs), createOptions));
        }

        /// <summary>
        /// Drops the index given a field name
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="indexName">The name of the index</param>
        /// <param name="partitionKey">An optional partition key</param>
        public async virtual Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Indexes
                .DropOneAsync(indexName);
        }

    }
}
