using CRM.IdentityServer.Entities;
using System;

namespace CRM.IdentityServer.Repositories {
    public interface IRefreshTokenRepository {
        void AddRefreshToken(RefreshToken refreshToken);
        long RemoveRefreshToken(Guid id);
        long RemoveRefreshToken(Guid userId, string clientId);
    }
}
