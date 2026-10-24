using CRM.Core.MongoCore;
using CRM.Logging.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Logging.Repositories {
    public class MongoLogRepository : BaseMongoRepository, IMongoLogRepository {

        public MongoLogRepository(string connectionString)
            : base(connectionString, "LogDb") {
        }

        public async Task<IList<DbLog>> GetMongoDbLogsAsync(string ident) {
            return await GetAllAsync<DbLog>(c => c.Ident == ident);
        }

        public async Task<IList<DbLog>> GetMongoDbLogsAsync(DateTime fromDate, DateTime toDate) {
            return await GetAllAsync<DbLog>(c => c.CreatedDate >= fromDate && c.CreatedDate <= toDate);
        }

        public void AddLogAsync(DbLog document) {
            AddOne<DbLog>(document);
        }

        public long DeleteLogAsync(int days) {
            var result = DeleteMany<DbLog>(c => c.CreatedDate <= DateTime.Now.AddDays(-days));

            return result;
        }
    }
}
