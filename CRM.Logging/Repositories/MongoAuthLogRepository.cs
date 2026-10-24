using CRM.Core.MongoCore;
using CRM.Logging.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Logging.Repositories {
    public class MongoAuthLogRepository : BaseMongoRepository, IMongoAuthLogRepository{

        public MongoAuthLogRepository(string connectionString)
            : base(connectionString, "LogDb") { }

        public async Task<IList<AuthLog>> GetMongoAuthLogsAsync(string userId) {
            return await GetAllAsync<AuthLog>(c => c.UserId == userId);
        }

        public async Task<IList<AuthLog>> GetMongoAuthLogsAsync(DateTime fromDate, DateTime toDate) {
            return await GetAllAsync<AuthLog>(c => c.CreatedDate >= fromDate && c.CreatedDate <= toDate);
        }

        public void AddLog(AuthLog document) {
            AddOne<AuthLog>(document);
        }

        public long DeleteLogAsync(string id) {
            var count = DeleteOne<AuthLog>(c => c.Id == id);

            return count;
        }

        public long DeleteLogAsync(int days) {
            var result = DeleteMany<DbLog>(c => c.CreatedDate <= DateTime.Now.AddDays(-days));

            return result;
        }
    }
}
