using CRM.Business.Common;
using CRM.Business.SystemUsers;
using CRM.Entities.MongoEntities.SystemUserNameSpace;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.SystemUserRepositories {
    public interface ISystemUserRepository { 
        Task<SystemUser> GetSystemUserAsync(string id);
        Task<IList<SystemUser>> GetSystemUsersAsync();
        Task<IList<SystemUser>> GetSystemUsersAsync(bool isActive);
        Task<IList<SystemUser>> GetSystemUsersbyOriginAsync(string origin);

        Task<IList<SystemUser>> GetSystemUsersAsync(int skip, int take);
        Task<SystemUserResponse> GetSystemUsersAsync(SystemUserSearchCriteria SystemUserCriteria, int skip, int take);
        Task<CommonResponse> InsertSystemUserAsync(SystemUser systemUser);
        Task<CommonResponse> UpdateSystemUserAsync(SystemUser systemUser);
        Task<long> DeleteSystemUserAsync(string id);
}
}
