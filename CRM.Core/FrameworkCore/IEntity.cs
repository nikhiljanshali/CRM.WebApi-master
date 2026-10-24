using System;

namespace CRM.Core.FrameworkCore {
    public interface IEntity<TKey> where TKey : IEquatable<TKey> {

        /// <summary>
        /// The Primary Key, which must be decorated with the [BsonId] attribute 
        /// if you want the MongoDb C# driver to consider it to be the document ID.
        /// </summary>        
        TKey Id { get; set; }

        /// <summary>
        /// A version number, to indicate the version of the schema.
        /// </summary>
        abstract int Version { get; set; }

        /// <summary>
        /// The user who created this document
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The datetime at which the document was created.
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// The user who updated document.
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// The datetime at which the document was updated.
        /// </summary>
        DateTime? UpdatedAt { get; set; }

    }

    public interface IEntity : IEntity<string> { }
}