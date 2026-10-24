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
    public abstract partial class BaseMongoRepository<TKey> : IBaseMongoRepository_Delete<TKey> {

        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteOne<TDocument>(TDocument document)
            where TDocument : IDocument<TKey> {
            var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);

            return HandlePartitioned<TDocument, TKey>(document)
                .DeleteOne(filter)
                .DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteOneAsync<TDocument>(TDocument document)
            where TDocument : IDocument<TKey> {
            var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);

            return (await HandlePartitioned<TDocument, TKey>(document)
                .DeleteOneAsync(filter))
                .DeletedCount;
        }

        /// <summary>
        /// Deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .DeleteOne(filter).DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return (await HandlePartitioned<TDocument, TKey>(partitionKey)
                .DeleteOneAsync(filter))
                .DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return (await HandlePartitioned<TDocument, TKey>(partitionKey)
                .DeleteManyAsync(filter))
                .DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey> {

            if (!documents.Any()) 
                return 0;
            
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument)) {
                long deleteCount = 0;
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey)) {
                    var groupIdsTodelete = group.Select(e => e.Id).ToArray();
                    deleteCount += (await HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).DeleteManyAsync(x => groupIdsTodelete.Contains(x.Id))).DeletedCount;
                }
                return deleteCount;
            }
            else {
                var idsTodelete = documents.Select(e => e.Id).ToArray();
                return (await HandlePartitioned<TDocument, TKey>(documents.FirstOrDefault()).DeleteManyAsync(x => idsTodelete.Contains(x.Id))).DeletedCount;
            }
        }

        /// <summary>
        /// Deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey> {

            if (!documents.Any()) 
                return 0;
            
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument)) {
                long deleteCount = 0;
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey)) {
                    var groupIdsTodelete = group.Select(e => e.Id).ToArray();
                    deleteCount += (HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).DeleteMany(x => groupIdsTodelete.Contains(x.Id))).DeletedCount;
                }
                return deleteCount;
            }
            else {
                var idsTodelete = documents.Select(e => e.Id).ToArray();
                return (HandlePartitioned<TDocument, TKey>(documents.FirstOrDefault()).DeleteMany(x => idsTodelete.Contains(x.Id))).DeletedCount;
            }
        }

        /// <summary>
        /// Deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .DeleteMany(filter)
                .DeletedCount;
        }

    }
}
