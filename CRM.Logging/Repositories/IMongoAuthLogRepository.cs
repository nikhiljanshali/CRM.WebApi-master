using CRM.Core.MongoCore;
using CRM.Logging.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Logging.Repositories {
    public interface IMongoAuthLogRepository : IBaseMongoRepository{
        Task<IList<AuthLog>> GetMongoAuthLogsAsync(string userId);
        Task<IList<AuthLog>> GetMongoAuthLogsAsync(DateTime fromDate, DateTime toDate);
        void AddLog(AuthLog document);
        long DeleteLogAsync(string id);
        long DeleteLogAsync(int days);
    }
}
