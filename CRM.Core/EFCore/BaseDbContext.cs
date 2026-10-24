using CRM.Core.EFCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CRM.Core.EFCore {
    public abstract partial class BaseDbContext : DbContext, IBaseDbContext {

        protected BaseDbContext(DbContextOptions options)
            : base(options) {
            base.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public void CommitChange() {
            base.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class {
            if (base.Entry(entity).State == EntityState.Detached)
                base.Set<T>().Attach(entity);

            base.Set<T>().Remove(entity);
        }

        public void DeleteRange<T>(IList<T> entities) where T : class {
            foreach (var entity in entities) {
                if (base.Entry(entity).State == EntityState.Detached)
                    base.Set<T>().Attach(entity);
            }

            base.Set<T>().RemoveRange(entities);
        }
    }
}