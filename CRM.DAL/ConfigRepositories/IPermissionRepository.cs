using CRM.Entities.MongoEntities.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public interface IPermissionRepository {
        Task<IList<OriginPermission>> GetAllOriginPermissionsAsync();
        Task<OriginPermission> GetOriginPermissionsAsync(string origin);
        Task<DeptPermission> GetDeptPermissionAsync(string departmentId);
        Task<EmpPermission> GetEmpPermissionAsync(string employeeId);

        Task<string> InsertOriginPermissionAsync(OriginPermission permission);
        Task<string> InsertDeptPermissionAsync(DeptPermission permission);
        Task<string> InsertEmpPermissionAsync(EmpPermission permission);

        Task<bool> UpdateOriginPermissionAsync(OriginPermission permission);
        Task<bool> UpdateDeptPermissionAsync(DeptPermission permission);
        Task<bool> UpdateEmpPermissionAsync(EmpPermission permission);

        Task<long> DeleteEmpPermissionAsync(string id);

    }
}
