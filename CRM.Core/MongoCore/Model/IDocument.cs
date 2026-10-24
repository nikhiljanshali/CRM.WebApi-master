using CRM.Core.FrameworkCore;
using System;

namespace CRM.Core.MongoCore.Model {

    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public interface IDocument<TKey> : IEntity<TKey> where TKey : IEquatable<TKey> { }

    /// <summary>
    /// This class represents a basic document that can be stored in MongoDb.
    /// Your document must implement this class in order for the MongoDbRepository to handle them.
    /// </summary>
    public interface IDocument : IDocument<string> { }
}
