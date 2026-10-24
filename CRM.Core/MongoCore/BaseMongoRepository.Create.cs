using CRM.Core.MongoCore.Abstractions;
using CRM.Core.MongoCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Core.MongoCore {

    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository : IBaseMongoRepository_Create {

        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task AddOneAsync<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey> {

            FormatDocument<TDocument, TKey>(document);
            await HandlePartitioned<TDocument, TKey>(document)
                .InsertOneAsync(document, null, cancellationToken);
        }

        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<string> {

            FormatDocument<TDocument, string>(document);
            await HandlePartitioned<TDocument, string>(document)
                .InsertOneAsync(document, null, cancellationToken);
        }

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        public virtual void AddOne<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey> {

            FormatDocument<TDocument, TKey>(document);
            HandlePartitioned<TDocument, TKey>(document)
                .InsertOne(document);
        }

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        public virtual void AddOne<TDocument>(TDocument document) where TDocument : IDocument<string> {

            FormatDocument<TDocument, string>(document);
            HandlePartitioned<TDocument, string>(document)
                .InsertOne(document);
        }

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task AddManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey> {

            if (!documents.Any()) 
                return;
            
            foreach (var document in documents) 
                FormatDocument<TDocument, TKey>(document);
            
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument)) {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey)) 
                    await HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertManyAsync(group.ToList(), null, cancellationToken);                
            }
            else 
                await GetCollection<TDocument, TKey>().InsertManyAsync(documents.ToList(), null, cancellationToken);
            
        }

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
            where TDocument : IDocument<string> {

            if (!documents.Any()) 
                return;
            
            foreach (var document in documents) 
                FormatDocument<TDocument, string>(document);
            
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument)) {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey)) 
                    await HandlePartitioned<TDocument, string>(group.FirstOrDefault()).InsertManyAsync(group.ToList(), null, cancellationToken);                
            }
            else 
                await GetCollection<TDocument, string>().InsertManyAsync(documents.ToList(), null, cancellationToken);
            
        }

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public virtual void AddMany<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey> {

            if (!documents.Any()) 
                return;
            
            foreach (var document in documents) 
                FormatDocument<TDocument, TKey>(document);
            
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument)) {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey)) 
                    HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertMany(group.ToList());                
            }
            else 
                GetCollection<TDocument, TKey>().InsertMany(documents.ToList());
            
        }

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<string> {

            if (!documents.Any())
                return;

            foreach (var document in documents)
                FormatDocument<TDocument, string>(document);

            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument)) {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                    HandlePartitioned<TDocument, string>(group.FirstOrDefault()).InsertMany(group.ToList());
            }
            else
                GetCollection<TDocument, string>().InsertMany(documents.ToList());
        }

    }
}
