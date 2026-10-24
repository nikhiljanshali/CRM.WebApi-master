using CRM.IdentityServer.Entities;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.IdentityServer.Repositories {
    public class RefreshTokenRepository : BaseMongoRepository, IRefreshTokenRepository {

        public RefreshTokenRepository(string connectionString, string database)
            :base(connectionString, database) { }

        public void AddRefreshToken(RefreshToken refreshToken) {
            AddOne<RefreshToken>(refreshToken);
        }

        public long RemoveRefreshToken(Guid id) {
            long result = 0;

            var token = GetById<RefreshToken>(id);
            if (token != null)
                result = DeleteOne<RefreshToken>(token);

            return result;
        }
        public long RemoveRefreshToken(Guid userId, string clientId) {
            long result = 0;

            var token = GetOne<RefreshToken>(c => c.UserId == userId && c.ClientId == clientId);
            if (token != null)
                result = DeleteOne<RefreshToken>(token);

            return result;
        }
    }
}
