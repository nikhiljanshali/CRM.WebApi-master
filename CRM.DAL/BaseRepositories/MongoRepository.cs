using CRM.Core.MongoCore;

namespace CRM.DAL.BaseRepositories {
    public class MongoRepository : BaseMongoRepository, IMongoRepository{

        public MongoRepository(string connectionString)
            : base(connectionString) {
        }

    }
}
