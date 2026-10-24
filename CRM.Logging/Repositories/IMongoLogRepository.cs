using CRM.Core.MongoCore;
using CRM.Logging.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Logging.Repositories {
    public interface IMongoLogRepository : IBaseMongoRepository {
        Task<IList<DbLog>> GetMongoDbLogsAsync(string ident);
        Task<IList<DbLog>> GetMongoDbLogsAsync(DateTime fromDate, DateTime toDate);
        void AddLogAsync(DbLog document);
        long DeleteLogAsync(int days);
    }
}
