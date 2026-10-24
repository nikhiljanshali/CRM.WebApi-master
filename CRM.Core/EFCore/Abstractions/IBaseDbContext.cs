using System.Collections.Generic;

namespace CRM.Core.EFCore.Abstractions {
    public interface IBaseDbContext {
        void CommitChange();
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(IList<T> entities) where T : class;
    }
}