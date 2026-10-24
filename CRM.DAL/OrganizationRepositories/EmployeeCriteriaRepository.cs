using CRM.Business.Common;
using CRM.DAL.BaseRepositories;
using CRM.DAL.ConfigRepositories;
using CRM.Entities.MongoEntities.Organization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public class EmployeeCriteriaRepository : IEmployeeCriteriaRepository {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMongoRepository _repository;

        public EmployeeCriteriaRepository(IMongoRepository repository, IPermissionRepository permissionRepository) {
            _repository = repository;
            _permissionRepository = permissionRepository;
        }

        public async Task<EmployeeCriteria> GetEmployeeCriteriaAsync(string id) {
            return await _repository.GetOneAsync<EmployeeCriteria>(c => c.Id == id);
        }

        public async Task<IList<EmployeeCriteria>> GetEmployeeCriteriaByEmployeeAsync(string id) {
            return await _repository.GetAllAsync<EmployeeCriteria>(c => c.Employee.Id == id);
        }

        public async Task<IList<EmployeeCriteria>> GetEmployeeCriteriaAsync() {
            return await _repository.GetAllAsync<EmployeeCriteria>(c => c.Id != null);
        }

        public async Task<string> InsertEmployeeCriteriaAsync(EmployeeCriteria employeeCriteria) {
            try {
                await _repository.AddOneAsync(employeeCriteria);

                return employeeCriteria.Id;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateEmployeeCriteriaAsync(EmployeeCriteria employeeCriteria) {
            try {
                employeeCriteria.UpdatedAt = DateTime.UtcNow;

                var result = await _repository.UpdateOneAsync(employeeCriteria);

                return result;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<long> DeleteEmployeeCriteriaAsync(string id) {
            return await _repository.DeleteOneAsync<EmployeeCriteria>(c => c.Id == id);
        }
    }
}
