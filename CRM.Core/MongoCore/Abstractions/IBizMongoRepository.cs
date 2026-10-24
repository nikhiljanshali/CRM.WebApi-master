using CRM.Core.FrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.Core.MongoCore {
    /// <summary>
    /// This contract is mongo specific; extends the generic one. it's all about being more flexible in cases of incorporating more database
    /// into the system say couchbase of arangodb as well.
    /// </summary>
    public interface IBizMongoRepository : IRepository {

        /// <summary>
        /// Set this property to true, when any DML operation is to be called from the QueueProcessor.
        /// </summary>
        bool IsProcessedByQueue { get; set; }

        /// <summary>
        /// Updating a document. Please note that the version number is mandatory to be passed.
        /// Check the implemention in BizMongoRepository.
        /// </summary>
        /// <typeparam name="T">Mongo Entity</typeparam>
        /// <param name="filter">FilterDefination</param>
        /// <param name="update">UpdateDefination</param>
        /// <returns>True | False</returns>
        Task<bool> Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity;

        /// <summary>
        /// Updating a document. Please note that the version number is mandatory to be passed.
        /// Check the implemention in BizMongoRepository.
        /// </summary>
        /// <typeparam name="T">Mongo Entity</typeparam>
        /// <param name="filter">FilterDefination</param>
        /// <param name="document">Document for updation</param>
        /// <param name="collectionName">Collection Name </param>
        /// <returns>True | False</returns>
        Task<bool> Update<T>(FilterDefinition<T> filter, T document, string collectionName) where T : BsonDocument;

        /// <summary>
        /// Bulk update based on the Query and Update commands.
        /// </summary>
        /// <typeparam name="T">Mongo Entity</typeparam>
        /// <param name="filter">FilterDefination</param>
        /// <param name="update">UpdateDefination</param>
        /// <returns>Count of updated document</returns>
        Task<int> BulkUpdate<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity;

        /// <summary>
        /// Delete based on the Query and Update commands.
        /// </summary>
        /// <typeparam name="T">Mongo Entity</typeparam>
        /// <param name="filter">FilterDefination</param>
        /// <param name="update">UpdateDefination</param>
        /// <returns>Count of deleted document</returns>
        Task<int> Delete<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity;

        /// <summary>
        /// Bulk delete. Though IMongoQuery can be generalized using an adapter interface, but it's fine for now.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">MongoQuery: an example could be something like this; Query<T>.In<string>(e => e.Id, ids).
        /// To delete all documents, set Query as Query.Null</param>
        /// <returns>Count of deleted documet</returns>
        Task<long> BulkDelete<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity;

        /// <summary>
        /// Get many documents with skip and take supported by mongodb driver's filter builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>Returns result of T</returns>
        Task<IList<T>> GetMany<T>(FilterDefinition<T> filter, int take = -1, int skip = 0) where T : IEntity;

        /// <summary>
        /// Get many documents supported by mongodb driver's filter builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns result of T</returns>
        Task<IList<T>> GetMany<T>(FilterDefinition<T> filter) where T : IEntity;

        /// <summary>
        /// Equivalent to a term query in lucene; a great feature. Wild card serches are not supported at the moment with a text index.
        /// Also do not forget to create a text index on the mongo collection referred; eg. db.Author.ensureIndex({nm : "text"})
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="term"></param>
        /// <param name="take">-1 here basically means take All</param>
        /// <param name="skip"></param>
        /// <returns>Returns result of T</returns>
        Task<IList<T>> GetManyByTextSearch<T>(string term, int take = -1, int skip = 0) where T : IEntity;

        /// <summary>
        /// Get many documents with projection in other object.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        Task<IList<TResult>> GetMany<T, TResult>(FilterDefinition<T> filter, Expression<Func<T, TResult>> project) where T : IEntity;

        /// <summary>
        /// Returns a single pair of DenormalizedReference type.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDenormalizedReference"></typeparam>
        /// <param name="Id"></param>
        /// <returns>Denormalized Document</returns>
        Task<TDenormalizedReference> GetOptionById<TEntity, TDenormalizedReference>(string Id)
            where TEntity : IEntity
            where TDenormalizedReference : IDenormalizedReference, new();

        /// <summary>
        /// Returns a list of DenormalizedReference types
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDenormalizedReference"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="take">"-1" hele would actually default ot 256 records</param>
        /// <param name="skip"></param>
        /// <returns>List of Denormalized Documents</returns>
        Task<IList<TDenormalizedReference>> GetOptionSet<TEntity, TDenormalizedReference>(Expression<Func<TEntity, bool>> predicate = null, int take = -1, int skip = 0)
            where TEntity : IEntity
            where TDenormalizedReference : IDenormalizedReference, new();

        /// <summary>
        /// Returns pure bsondocuments.
        /// </summary>
        /// <typeparam name="T">BsonDocument</typeparam>
        /// <param name="filter">filter query</param>
        /// <param name="collectionName">Collection Name</param>
        /// <returns>Return BsonDocuments</returns>
        Task<IList<T>> GetBsonDocuments<T>(FilterDefinition<T> filter, string collectionName) where T : BsonDocument;

        /// <summary>
        /// Get historical documents
        /// </summary>
        /// <typeparam name="T">Type of Document</typeparam>
        /// <param name="id">Document unique id</param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>return list of historical documents</returns>
        Task<IList<T>> GetHistory<T>(string id, int take = -1, int skip = 0) where T : IEntity;

        /// <summary>
        /// Insert into Historical document hive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void InsertOneIntoHistory<T>(T entity) where T : IEntity;

        void InsertManyIntoHistory<T>(IList<T> entities) where T : IEntity;

        void SetDocumentDefaults(IEntity entity);

        void SetDocumentDefaults<T>(IList<T> entities) where T : IEntity;
          
    }
}
