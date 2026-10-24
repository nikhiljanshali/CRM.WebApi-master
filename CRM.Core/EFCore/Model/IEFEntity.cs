using CRM.Core.FrameworkCore;
using System;

namespace CRM.Core.EFCore.Model {
    /// <summary>
    /// This class represents a basic table that can be stored in MSSql.
    /// Your EF entity must implement this class in order for the DbContext to handle them.
    /// </summary>
    public interface IEFEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey> { }

    /// <summary>
    /// This class represents a basic table that can be stored in MSSql.
    /// Your EF entity must implement this class in order for the DbContext to handle them.
    /// </summary>
    public interface IEFEntity : IEFEntity<int> { }
}