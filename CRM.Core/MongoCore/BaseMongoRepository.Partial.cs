using CRM.Core.FrameworkCore;
using CRM.Core.MongoCore.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.Core.MongoCore {
    public abstract partial class BaseMongoRepository : IBizMongoRepository{

        protected readonly int takeCount = 256000;

        /// <summary>
        /// Flag for queue proccesing by RabbitMQ 
        /// </summary>
        public bool IsProcessedByQueue { get; set; }

        /// <summary>
        /// Default date in UTC
        /// </summary>
        public DateTime CurrentDate {
            get { return DateTime.Now.ToUniversalTime(); }
        }

        #region "Interface implementaions; generic CRUD repository"

        #region "Insert"

        /// <summary>
        /// Insert single document
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="entity">Document</param>
        /// <returns>Id of inserted document</returns>
        public virtual async Task<string> Insert<T>(T entity) where T : IEntity {
            if (!IsProcessedByQueue)
                SetDocumentDefaults(entity);

            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            //The default WriteConcern here is "Acknowledged". Go ahead and override this method in particular repositories
            //if you need other ways of writing to a mongo collection.
            await collection.InsertOneAsync(entity);

            return entity.Id;
        }

        /// <summary>
        /// Insert multiple documents
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="entities">List of documents</param>
        /// <returns>List of IDs of the generated documents</returns>
        public virtual async Task<IList<string>> Insert<T>(IList<T> entities) where T : IEntity {
            if (!IsProcessedByQueue)
                SetDocumentDefaults(entities);

            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);
            await collection.InsertManyAsync(entities);

            return entities.Select(c => c.Id).ToList();
        }

        /// <summary>
        /// Bulk insert, very useful in cases of data migration
        /// </summary>
        /// <typeparam name="T">Type of Document</typeparam>
        /// <param name="entities"></param>
        /// <returns>List of IDs of the generated documents</returns>
        public virtual async Task<IList<string>> BulkInsert<T>(IList<T> entities) where T : IEntity {
            if (!IsProcessedByQueue)
                SetDocumentDefaults(entities);

            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            List<WriteModel<T>> Docs = new List<WriteModel<T>>();
            foreach (var entity in entities)
                Docs.Add(new InsertOneModel<T>(entity));

            await collection.BulkWriteAsync(Docs, new BulkWriteOptions { IsOrdered = false });

            return entities.Select(c => c.Id).ToList();
        }

        #endregion

        #region GET

        /// <summary>
        /// Find one document by Id
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="id">id of document</param>
        /// <returns>Document match by id</returns>
        public virtual async Task<T> GetOne<T>(string id) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            return await collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find one by Predicate
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="predicate">predicate for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <returns>T</returns>
        public virtual async Task<T> GetOne<T>(Expression<Func<T, bool>> predicate) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            return await collection.Find(predicate)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find one By predicate
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="predicate">predicate for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <returns>Projection match by predicate</returns>
        public virtual async Task<TResult> GetOne<T, TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> project)
            where T : IEntity
            where TResult : class {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            return await collection.Find(predicate)
                .Project(project)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Load records based on predicates
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="predicate">Use the Predicate object to build predicates. Default predicate is Id not Null</param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>List of T</returns>
        public virtual async Task<IList<T>> GetMany<T>(Expression<Func<T, bool>> predicate = null, int take = -1, int skip = 0) where T : IEntity {
            if (take == -1)
                take = takeCount;

            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            if (predicate == null) {
                return await collection.Find(c => c.Id != null)
                    .Skip(skip)
                    .Limit(take)
                    .ToListAsync();
            }
            else {
                return await collection.Find(predicate)
                    .Skip(skip)
                    .Limit(take)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Get multiple documents by filter with take and skip support
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="filter">filter for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>List of T</returns>
        public virtual async Task<IList<T>> GetMany<T>(FilterDefinition<T> filter, int take = -1, int skip = 0) where T : IEntity {
            if (take == -1)
                take = takeCount;

            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            return await collection.Find(filter)
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
        }

        /// <summary>
        /// Get multiple documents by filter
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="filter">filter for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <returns>List of T</returns>
        public virtual async Task<IList<T>> GetMany<T>(FilterDefinition<T> filter) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            return await collection.Find(filter)
                .ToListAsync();
        }

        /// <summary>
        /// Load records based on predicates and projecion
        /// </summary>
        /// <typeparam name="TResult">Projection object</typeparam>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="predicate">predicte for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <param name="project">Projection for convert T in other object</param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>List of projection class</returns>
        public virtual async Task<IList<TResult>> GetMany<T, TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> project, int take = -1, int skip = 0)
            where T : IEntity
            where TResult : class {
            if (take == -1)
                take = takeCount;

            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            if (predicate == null) {
                return await collection.Find(c => c.Id != null)
                    .Project(project)
                    .Skip(skip)
                    .Limit(take)
                    .ToListAsync();
            }
            else {
                return await collection.Find(predicate)
                    .Project(project)
                    .Skip(skip)
                    .Limit(take)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Load records based on predicates and projecion
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <typeparam name="TResult">Projection object</typeparam>
        /// <param name="filter">filter for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <param name="project">Projection for convert T in other object</param>
        /// <returns>List of projection class</returns>
        public virtual async Task<IList<TResult>> GetMany<T, TResult>(FilterDefinition<T> filter, Expression<Func<T, TResult>> project) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            return await collection.Find(filter)
                .Project(project)
                .ToListAsync();
        }

        /// <summary>
        /// Returns of bsonDocuments
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="filter">filter for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <param name="collectionName"></param>
        /// <returns>List of bsonDocuments</returns>
        public virtual async Task<IList<T>> GetBsonDocuments<T>(FilterDefinition<T> filter, string collectionName) where T : BsonDocument {
            var collection = MongoDbContext.GetCollection<T>(collectionName);

            return await collection.Find(filter)
                .ToListAsync();
        }

        #endregion

        #region Update

        /// <summary>
        /// Update for complete object graph; when all fields are supplied. Else, do it the other way mentioned in "ClientRepository.Update() method"
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="entity">Document</param>
        /// <returns>Flags of acknowledge</returns>
        public virtual async Task<bool> Update<T>(T entity) where T : IEntity {
            var collectionName = typeof(T).Name;

            var collection = MongoDbContext.GetCollection<T>(collectionName);

            var doc = await collection.Find(c => c.Id == entity.Id).SingleOrDefaultAsync();
            var currentVersion = doc.Version;

            if (entity.Version == currentVersion) { //handling concurrency; the request having the correct document version would win.
                if (!IsProcessedByQueue) 
                    entity.UpdatedAt = CurrentDate;

                entity.Version++;
                var result = await collection.ReplaceOneAsync<T>(c => c.Id == entity.Id, entity);

                return result.IsAcknowledged;
            }

            return false;
        }

        /// <summary>
        /// Update documents field by update definition
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="filter">filter for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <param name="update">Update defination for updating field</param>
        /// <returns>Flags of acknowledge</returns>
        public virtual async Task<bool> Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity {
            var collectionName = typeof(T).Name;

            try {
                var collection = MongoDbContext.GetCollection<T>(collectionName);

                update.Inc(c => c.Version, 1);
                update.Set(c => c.UpdatedAt, CurrentDate);

                var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });

                var doc = await collection.Find(filter).FirstOrDefaultAsync();
                if (doc != null)
                    await Task.Factory.StartNew(() => InsertOneIntoHistory<T>(doc));

                return result.IsAcknowledged;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Bulk update based on FilterDefinition and UpdateDefinition.
        /// To process by a queue please set FilterDefinition and UpdateDefinition build yourself, as in all other Insert and update operations 
        /// you set the particular Entity. Though I've never seen people updating in bulk in most business scenarios.
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="filter">filter for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <param name="update">Update defination for updating field</param>
        /// <returns>Count of updated documents</returns>
        public virtual async Task<long> BulkUpdate<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity {
            var collectionName = typeof(T).Name;
            long modifiedCount = 0;

            try {
                var collection = MongoDbContext.GetCollection<T>(collectionName);

                if (!IsProcessedByQueue) {
                    //set defaults
                    update.Inc(c => c.Version, 1);
                    update.Set(c => c.UpdatedAt, CurrentDate);
                }

                var result = await collection.UpdateManyAsync(filter, update, new UpdateOptions { IsUpsert = true });

                if (result.IsModifiedCountAvailable)
                    modifiedCount = result.ModifiedCount;

                var docs = await collection.Find<T>(filter).ToListAsync();

                //Insert into history
                await Task.Factory.StartNew(() => InsertManyIntoHistory<T>(docs));
            }
            catch (Exception ex) {
                var msg = ex.Message;
            }

            return modifiedCount;
        }

        /// <summary>
        /// Update documents field by update definition
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="filter">filter for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <param name="document">Document</param>
        /// <param name="collectionName">Collection Name</param>
        /// <returns>Flags of acknowledge</returns>
        public virtual async Task<bool> Update<T>(FilterDefinition<T> filter, T document, string collectionName) where T : BsonDocument {
            try {
                var collection = MongoDbContext.GetCollection<T>(collectionName);
                var result = await collection.UpdateOneAsync(filter, document, new UpdateOptions { IsUpsert = true });

                return result.IsAcknowledged;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete by Id. This is a paramanent delete from the collection, but a document is inserted into history.
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="id">Document Id</param>
        /// <returns>Flag of Acknowledge</returns>
        public virtual async Task<bool> Delete<T>(string id) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            var doc = await GetOne<T>(id);

            if (!IsProcessedByQueue) {
                doc.Version = -1; // -1 means deleted here.
                doc.UpdatedAt = CurrentDate;
            }

            //Insert an entry into history first
            await Task.Factory.StartNew(() => InsertOneIntoHistory<T>(doc) );

            var filter = Builders<T>.Filter.Eq(c => c.Id, id);
            var result = await collection.DeleteOneAsync(filter);

            return result.IsAcknowledged;
        }

        /// <summary>
        /// Deletes by Ids for a smaller batch size.
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="ids">Ids of Documents</param>
        /// <returns>Flag of Acknowledge</returns>
        public virtual async Task<bool> Delete<T>(IList<string> ids) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            var filter = Builders<T>.Filter.In(c => c.Id, ids);
            var docs = await collection.Find(filter).ToListAsync();

            if (!IsProcessedByQueue) {
                foreach (var doc in docs) {
                    doc.Version = -1; // -1 means deleted here
                    doc.UpdatedAt = CurrentDate;
                }
            }

            await Task.Factory.StartNew(() => InsertManyIntoHistory<T>(docs));
            var result = await collection.DeleteManyAsync(filter);

            return result.IsAcknowledged;
        }

        /// <summary>
        /// Delete based on the Query and Update commands.
        /// </summary>
        /// <typeparam name="T">Mongo Entity</typeparam>
        /// <param name="filter">FilterDefination</param>
        /// <param name="update">UpdateDefination</param>
        /// <returns>Count of deleted document</returns>
        public virtual async Task<long> Delete<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity {
            var collectionName = typeof(T).Name;
            long modifiedCount = 0;

            try {
                var collection = MongoDbContext.GetCollection<T>(collectionName);

                if (!IsProcessedByQueue) 
                    update.Set(c => c.UpdatedAt, CurrentDate);                

                var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });

                if (result.IsModifiedCountAvailable)
                    modifiedCount = result.ModifiedCount;
            }
            catch (Exception ex) {
                var msg = ex.Message;
            }

            return modifiedCount;
        }

        /// <summary>
        /// Bulk delete (not supported in 2.0 driver)
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="query">FilterDefinition: an example could be something like this; Builders<T>.Filter.In(e => e.Id, ids). 
        /// To delete all documents, set Query as Query.Null</param> 
        /// <returns>Count of deleted document</returns>
        public virtual async Task<long> BulkDelete<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : IEntity {
            var collectionName = typeof(T).Name;
            long modifiedCount = 0;

            try {
                var collection = MongoDbContext.GetCollection<T>(collectionName);

                if (!IsProcessedByQueue) 
                    update.Set(c => c.UpdatedAt, CurrentDate);                

                var result = await collection.UpdateManyAsync(filter, update, new UpdateOptions { IsUpsert = true });

                if (result.IsModifiedCountAvailable)
                    modifiedCount = result.ModifiedCount;
            }
            catch (Exception ex) {
                var msg = ex.Message;
            }

            return modifiedCount;
        }

        /// <summary>
        /// Drop connected database
        /// </summary>
        /// <returns>Flag of Acknowledge</returns>
        public virtual async Task<bool> DropDatabase() {
            await MongoDbContext.Client.DropDatabaseAsync(MongoDbContext.Database.DatabaseNamespace.DatabaseName);

            return true;
        }

        /// <summary>
        /// Drop entire database
        /// </summary>
        /// <param name="collectionName">Collection Name</param>
        /// <returns>Flag of Acknowledge</returns>
        public virtual bool DropCollection(string collectionName) {
            MongoDbContext.DropCollection(collectionName);

            return true;
        }

        #endregion

        #endregion

        #region Full text search

        /// <summary>
        /// Equivalent to a term query in lucene; a great feature. Wild card searches are not supported at the moment with a text index.
        /// Also do not forget to create a text index on the collection referred; eg. db.Author.ensureIndex({nm : "text"})
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="term">Text term for filter documents</param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>List of T</returns>
        public async Task<IList<T>> GetManyByTextSearch<T>(string term, int take = -1, int skip = 0) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            var filter = Builders<T>.Filter.Text(term);

            if (take == -1)
                return await collection.Find<T>(filter).Skip(skip).ToListAsync();
            else
                return await collection.Find<T>(filter).Skip(skip).Limit(take).ToListAsync();
        }

        #endregion

        #region "Other methods; GetCount(), GetOptionSets()"

        /// <summary>
        /// This is a default implementation. Returns a single pair of DenormalizedReference type.
        /// This can be overriden for specific repositories for different databases.
        /// </summary>
        /// <typeparam name="TEntity">Type of document</typeparam>
        /// <typeparam name="TDenormalizedReference"></typeparam>
        /// <param name="Id">Parent Id</param>
        /// <returns>Denormlized object</returns>
        public virtual async Task<TDenormalizedReference> GetOptionById<TEntity, TDenormalizedReference>(string Id)
            where TEntity : IEntity
            where TDenormalizedReference : IDenormalizedReference, new() {
            var collection = MongoDbContext.GetCollection<TEntity>(typeof(TEntity).Name);

            return await collection.Find(c => c.Id == Id)
                .Project(c => new TDenormalizedReference { DenormalizedId = c.Id })
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Returns a list of DenormalizedReference types; Get optionSets based on a predicate.
        /// </summary>
        /// <typeparam name="TEntity">Type of document</typeparam>
        /// <typeparam name="TDenormalizedReference"></typeparam>
        /// <param name="predicate">the defalut predicate considered is the IsActive field to be true; (p => p.IsActive == true)</param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>Denormlized object</returns>
        public virtual async Task<IList<TDenormalizedReference>> GetOptionSet<TEntity, TDenormalizedReference>(Expression<Func<TEntity, bool>> predicate = null, int take = -1, int skip = 0)
            where TEntity : IEntity
            where TDenormalizedReference : IDenormalizedReference, new() {
            if (take == -1)
                take = takeCount;

            var collection = MongoDbContext.GetCollection<TEntity>(typeof(TEntity).Name);

            if (predicate == null) {
                return await collection.Aggregate()
                    .Project(c => new TDenormalizedReference { DenormalizedId = c.Id })
                    .Skip(skip)
                    .Limit(take)
                    .ToListAsync();
            }
            else {
                return await collection.Aggregate()
                    .Match(predicate)
                    .Project(c => new TDenormalizedReference { DenormalizedId = c.Id })
                    .Skip(skip)
                    .Limit(take)
                    .ToListAsync();
            }
        }

        /// <summary>
        /// Returns the count of records in a collection
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="predicate">Optional value is null. If predicate is null it count only the active records</param>
        /// <returns>Counts of documents</returns>
        public async Task<long> GetCount<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null) where T : IEntity {
            var collection = MongoDbContext.GetCollection<T>(typeof(T).Name);

            if (predicate == null)
                return await collection.CountDocumentsAsync(c => c.Id != null);
            else
                return await collection.CountDocumentsAsync(predicate);
        }

        #endregion

        #region "History and defaults"

        /// <summary>
        /// Get historical documents from collection
        /// </summary>
        /// <typeparam name="T">Type of document</typeparam>
        /// <param name="id"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>List of historical documents</returns>
        public async Task<IList<T>> GetHistory<T>(string id, int take = -1, int skip = 0) where T : IEntity {
            if (take == -1)
                take = takeCount;

            var collectionX = MongoDbContext.GetCollection<DocumentX<T>>(typeof(T).Name + 'X');

            var query = collectionX.Aggregate()
                .Match(c => c.XDocument.Id == id)
                .SortByDescending(c => c.XDocument.Version)
                .Skip(skip)
                .Limit(take)
                .Project(c => c.XDocument);

            var result = await query.ToListAsync();

            return result;
        }

        /// <summary>
        /// Insert document in historical collection
        /// </summary>
        /// <typeparam name="T">Type of docuement</typeparam>
        /// <param name="entity">Document to add in historical collection</param>
        public async void InsertOneIntoHistory<T>(T entity) where T : IEntity {
            DocumentX<T> xDoc = new DocumentX<T> {
                XDocument = entity
            };

            var collectionX = MongoDbContext.GetCollection<DocumentX<T>>(typeof(T).Name + 'X');

            await collectionX.InsertOneAsync(xDoc);
        }

        /// <summary>
        /// Insert multiple documents in historical collection
        /// </summary>
        /// <typeparam name="T">Type of Document</typeparam>
        /// <param name="entities">List of documents to add in historical collection</param>
        public async void InsertManyIntoHistory<T>(IList<T> entities) where T : IEntity {
            List<WriteModel<DocumentX<T>>> xDocs = new List<WriteModel<DocumentX<T>>>();

            foreach (var doc in entities) {
                DocumentX<T> xDoc = new DocumentX<T> {
                    XDocument = doc
                };
                xDocs.Add(new InsertOneModel<DocumentX<T>>(xDoc)); ;
            }

            var collectionX = MongoDbContext.GetCollection<DocumentX<T>>(typeof(T).Name + 'X');

            // Unorderd bulk operation for reducing server round trip.
            await collectionX.BulkWriteAsync(xDocs, new BulkWriteOptions { IsOrdered = false });
        }

        /// <summary>
        /// Setup default properties of Document
        /// </summary>
        /// <typeparam name="T">Type of Document</typeparam>
        /// <param name="entities"></param>
        public void SetDocumentDefaults(IEntity entity) {
            entity.Version = entity.Version + 1;
            entity.CreatedDate = CurrentDate;
            entity.UpdatedAt = CurrentDate;
        }

        /// <summary>
        /// Setup default properties of multiple Documents
        /// </summary>
        /// <typeparam name="T">Type of Document</typeparam>
        /// <param name="entities"></param>
        public void SetDocumentDefaults<T>(IList<T> entities) where T : IEntity {
            foreach (var entity in entities) 
                this.SetDocumentDefaults(entity);            
        }

        Task<int> IBizMongoRepository.BulkUpdate<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) {
            throw new NotImplementedException();
        }

        Task<int> IBizMongoRepository.Delete<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) {
            throw new NotImplementedException();
        }

        Task<int> IRepository.GetCount<T>(Expression<Func<T, bool>> predicate) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
