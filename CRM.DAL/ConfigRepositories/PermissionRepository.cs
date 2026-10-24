using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.ConfigRepositories {
    public class PermissionRepository : IPermissionRepository {
        private readonly IMongoRepository _repository;

        public PermissionRepository(IMongoRepository repository) {
            _repository = repository;
        }
           
        public async Task<IList<OriginPermission>> GetAllOriginPermissionsAsync() {
            return await _repository.GetAllAsync<OriginPermission>(c => c.Id != null);
        }

        public async Task<OriginPermission> GetOriginPermissionsAsync(string origin) {
            return await _repository.GetOneAsync<OriginPermission>(c => c.Origin == origin);
        }

        public async Task<DeptPermission> GetDeptPermissionAsync(string departmentId) {
            return await _repository.GetOneAsync<DeptPermission>(c => c.DepartmentId == departmentId);
        }

        public async Task<EmpPermission> GetEmpPermissionAsync(string employeeId) {
            return await _repository.GetOneAsync<EmpPermission>(c => c.EmployeeId == employeeId);
        }


        public async Task<string> InsertOriginPermissionAsync(OriginPermission permission) {
            var oldPermission = await _repository.GetOneAsync<OriginPermission>(c => c.Origin == permission.Origin);
            if (oldPermission != null)
                return "Duplication!";

            await _repository.AddOneAsync(permission);
            return permission.Id;
        }

        public async Task<string> InsertDeptPermissionAsync(DeptPermission permission) {
            var oldPermission = await _repository.GetOneAsync<DeptPermission>(c => c.DepartmentId == permission.DepartmentId);
            if (oldPermission != null)
                return "Duplication!";

            await _repository.AddOneAsync(permission);
            return permission.Id;
        }

        public async Task<string> InsertEmpPermissionAsync(EmpPermission permission) {
            var oldPermission = await _repository.GetOneAsync<EmpPermission>(c => c.EmployeeId == permission.EmployeeId);
            if (oldPermission != null)
                return "Duplication!";

            await _repository.AddOneAsync(permission);
            return permission.Id;
        }


        public async Task<bool> UpdateOriginPermissionAsync(OriginPermission permission) {
            var result = await _repository.UpdateOneAsync(permission);

            return result;
        }

        public async Task<bool> UpdateDeptPermissionAsync(DeptPermission permission) {
            var result = await _repository.UpdateOneAsync(permission);

            return result;
        }

        public async Task<bool> UpdateEmpPermissionAsync(EmpPermission permission) {
            var result = await _repository.UpdateOneAsync(permission);

            return result;
        }


        public async Task<long> DeleteEmpPermissionAsync(string id) {
            return await _repository.DeleteOneAsync<EmpPermission>(c => c.Id == id);
        }

    }
}
