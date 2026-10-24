using CRM.Core.MongoCore.Abstractions;
using CRM.Core.MongoCore.Model;
using CRM.Core.Utils;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
    public abstract partial class ReadOnlyMongoRepository<TKey> : IReadOnlyMongoRepository<TKey> where TKey : IEquatable<TKey>{

        /// <summary>
        /// The connection string.
        /// </summary>
        public string ConnectionString { get; protected set; }

        /// <summary>
        /// The database name.
        /// </summary>
        public string DatabaseName { get; protected set; }

        /// <summary>
        /// The MongoDbContext
        /// </summary>
        protected IMongoDbContext MongoDbContext = null;
                

        /// <summary>
        /// The constructor taking a connection string.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        protected ReadOnlyMongoRepository(string connectionString) {
            SetupMongoDbContext(connectionString);
        }

        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected ReadOnlyMongoRepository(string connectionString, string databaseName) {
            SetupMongoDbContext(connectionString, databaseName);
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDbContext"/>.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext"/></param>
        protected ReadOnlyMongoRepository(IMongoDbContext mongoDbContext) {
            SetupMongoDbContext(mongoDbContext);
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase"/></param>
        protected ReadOnlyMongoRepository(IMongoDatabase mongoDatabase) : this(new MongoDbContext(mongoDatabase)) {
        }

        #region Setup Mongodb Context

        protected void SetupMongoDbContext(IMongoDbContext mongoDbContext) {
            MongoDbContext = MongoDbContext ?? mongoDbContext;
        }

        protected void SetupMongoDbContext(string connectionString) {
            var mongoUrl = new MongoUrl(connectionString);

            DatabaseName = DatabaseName ?? mongoUrl.DatabaseName;
            ConnectionString = connectionString;

            SetupMongoDbContext(new MongoDbContext(connectionString));
        }

        protected void SetupMongoDbContext(string connectionString, string databaseName) {
            if (databaseName == null) {
                var mongoUrl = new MongoUrl(connectionString);
                databaseName = databaseName ?? mongoUrl.DatabaseName;
            }

            ConnectionString = connectionString;
            DatabaseName = databaseName;

            SetupMongoDbContext(new MongoDbContext(connectionString, databaseName));
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Gets a IMongoQueryable for a potentially partitioned document type and a filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="Key">The type of the primary key.</typeparam>
        /// <param name="filter">The filter definition.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        public virtual IMongoQueryable<TDocument> GetQuery<TDocument, Key>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<Key>
            where Key : IEquatable<Key> {
            return GetCollection<TDocument, Key>(partitionKey).AsQueryable().Where(filter);
        }

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="Key">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument, Key>(TDocument document)
            where TDocument : IDocument<Key>
            where Key : IEquatable<Key> {
            if (document is IPartitionedDocument) {
                return GetCollection<TDocument, Key>(((IPartitionedDocument)document).PartitionKey);
            }
            return GetCollection<TDocument, Key>();
        }

        /// <summary>
        /// Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="Key">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> GetCollection<TDocument, Key>(string partitionKey = null)
            where TDocument : IDocument<Key>
            where Key : IEquatable<Key> {
            return MongoDbContext.GetCollection<TDocument>(partitionKey);
        }

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="Key">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument, Key>(string partitionKey)
            where TDocument : IDocument<Key>
            where Key : IEquatable<Key> {
            if (!string.IsNullOrEmpty(partitionKey)) {
                return GetCollection<TDocument, Key>(partitionKey);
            }
            return GetCollection<TDocument, Key>();
        }

        /// <summary>
        /// Converts a LINQ expression of TDocument, TValue to a LINQ expression of TDocument, object
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="expression">The expression to convert</param>
        protected virtual Expression<Func<TDocument, object>> ConvertExpression<TDocument, TValue>(Expression<Func<TDocument, TValue>> expression) {
            var param = expression.Parameters[0];
            Expression body = expression.Body;
            var convert = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<TDocument, object>>(convert, param);
        }

        /// <summary>
        /// Maps a IndexCreationOptions object to a MongoDB.Driver.CreateIndexOptions object
        /// </summary>
        /// <param name="indexCreationOptions">The options for creating an index.</param>
        /// <returns></returns>
        protected virtual CreateIndexOptions MapIndexOptions(IndexCreationOptions indexCreationOptions) {
            return new CreateIndexOptions {
                Unique = indexCreationOptions.Unique,
                TextIndexVersion = indexCreationOptions.TextIndexVersion,
                SphereIndexVersion = indexCreationOptions.SphereIndexVersion,
                Sparse = indexCreationOptions.Sparse,
                Name = indexCreationOptions.Name,
                Min = indexCreationOptions.Min,
                Max = indexCreationOptions.Max,
                LanguageOverride = indexCreationOptions.LanguageOverride,
                ExpireAfter = indexCreationOptions.ExpireAfter,
                DefaultLanguage = indexCreationOptions.DefaultLanguage,
                Bits = indexCreationOptions.Bits,
                Background = indexCreationOptions.Background,
                Version = indexCreationOptions.Version
            };
        }

        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="Key">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        protected virtual IFindFluent<TDocument, TDocument> GetMinMongoQuery<TDocument, Key, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
                    where TDocument : IDocument<Key>
                    where Key : IEquatable<Key> {
            return GetCollection<TDocument, Key>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                                                               .SortBy(ConvertExpression(minValueSelector))
                                                               .Limit(1);
        }

        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="Key">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        protected virtual IFindFluent<TDocument, TDocument> GetMaxMongoQuery<TDocument, Key, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
                    where TDocument : IDocument<Key>
                    where Key : IEquatable<Key> {
            return GetCollection<TDocument, Key>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                                                               .SortByDescending(ConvertExpression(maxValueSelector))
                                                               .Limit(1);
        }

        /// <summary>
        /// Sets the value of the document Id if it is not set already.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="Key">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        protected void FormatDocument<TDocument, Key>(TDocument document)
            where TDocument : IDocument<Key>
            where Key : IEquatable<Key> {
            if (document == null) {
                throw new ArgumentNullException(nameof(document));
            }
            var defaultTKey = default(Key);
            if (document.Id == null || (defaultTKey != null && defaultTKey.Equals(document.Id))) {
                document.Id = IdGenerator.GetId<Key>();
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<TDocument> GetByIdAsync<TDocument>(TKey id, string partitionKey = null) where TDocument : IDocument<TKey> {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);

            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public TDocument GetById<TDocument>(TKey id, string partitionKey = null) where TDocument : IDocument<TKey> {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);

            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns a collection cursor.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter);
        }

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            var count = await HandlePartitioned<TDocument, TKey>(partitionKey).CountDocumentsAsync(filter);
            return (count > 0);
        }

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            var count = HandlePartitioned<TDocument, TKey>(partitionKey).CountDocuments(filter);
            return (count > 0);
        }

        /// <summary>
        /// Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .ToListAsync();
        }

        /// <summary>
        /// Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .ToList();
        }

        /// <summary>
        /// Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public async Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .CountDocumentsAsync(filter);
        }

        /// <summary>
        /// Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .CountDocuments(filter);
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public async Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <returns></returns>
        public TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public async Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(minValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(minValueSelector)
                .Limit(1)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public async Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await GetMaxMongoQuery<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey)
                .Project(maxValueSelector)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return GetMaxMongoQuery<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey)
                .Project(maxValueSelector)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await GetMaxMongoQuery<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey)
                .Project(minValueSelector)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return GetMaxMongoQuery<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey)
                .Project(minValueSelector)
                .FirstOrDefault();
        }

        #endregion

        #region Maths

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual async Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await GetQuery<TDocument, TKey>(filter, partitionKey).SumAsync(selector);
        }

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual async Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await GetQuery<TDocument, TKey>(filter, partitionKey).SumAsync(selector);
        }

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual int SumBy<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return GetQuery<TDocument, TKey>(filter, partitionKey).Sum(selector);
        }

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual decimal SumBy<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return GetQuery<TDocument, TKey>(filter, partitionKey).Sum(selector);
        }

        #endregion Maths

        #region Project

        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Project(projection)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Project(projection)
                .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Project(projection)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Project(projection)
                .ToList();
        }


        #endregion Project

        #region Group

        /// <summary>
        /// Groups a collection of documents given a grouping criteria, 
        /// and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>( Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TProjection : class, new() {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Aggregate()
                .Group(groupingCriteria, groupProjection)
                .ToList();
        }

        /// <summary>
        /// Groups filtered a collection of documents given a grouping criteria, 
        /// and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>( Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
                where TDocument : IDocument<TKey>
                where TProjection : class, new() {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            return collection.Aggregate()
                .Match(Builders<TDocument>.Filter.Where(filter))
                .Group(groupingCriteria, groupProjection)
                .ToList();
        }

        #endregion

        #region Paginated

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="sortSelector">The property selector.</param>
        /// <param name="ascending">Order of the sorting.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>( Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> sortSelector, 
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null)
            where TDocument : IDocument<TKey> {
            var sorting = ascending
                ? Builders<TDocument>.Sort.Ascending(sortSelector)
                : Builders<TDocument>.Sort.Descending(sortSelector);

            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sorting)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="sortDefinition">The sort definition.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>( Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null)
            where TDocument : IDocument<TKey> {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync();
        }

        #endregion

    }
}
