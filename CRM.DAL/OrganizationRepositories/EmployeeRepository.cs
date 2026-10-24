using CRM.Business.Common;
using CRM.Business.Employees;
using CRM.DAL.BaseRepositories;
using CRM.DAL.ConfigRepositories;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CRM.Entities.MongoEntities.Configuration;
using System.Linq;

namespace CRM.DAL.OrganizationRepositories {
    public class EmployeeRepository : IEmployeeRepository {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMongoRepository _repository;

        public EmployeeRepository(IMongoRepository repository, IPermissionRepository permissionRepository) {
            _repository = repository;
            _permissionRepository = permissionRepository;
        }

        public async Task<Employee> GetEmployeeAsync(string id) {
            try {

                return await _repository.GetOneAsync<Employee>(c => c.Id == id);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<Employee>> GetEmployeesAsync() {
            try {

                return await _repository.GetAllAsync<Employee>(c => c.Id != null);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<Employee>> GetEmployeesAsync(bool isActive) {
            try {

                return await _repository.GetAllAsync<Employee>(c => c.IsActive == isActive);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<Employee>> GetEmployeesAsync(int skip, int take) {
            try {

                return await _repository.GetPaginatedAsync<Employee>(c => c.Id != null, skip, take);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<EmployeeResponse> GetEmployeesAsync(EmployeeSearchCriteria EmployeeCriteria, int skip, int take) {
            try {
                var empResponse = new EmployeeResponse();
                Expression<Func<Employee, bool>> filter = c =>
                (string.IsNullOrEmpty(EmployeeCriteria.FirstName) || c.Name.FirstName.ToLower().Contains(EmployeeCriteria.FirstName.ToLower()))
                 && (string.IsNullOrEmpty(EmployeeCriteria.LastName) || c.Name.LastName.ToLower().Contains(EmployeeCriteria.LastName.ToLower()))
                 && (string.IsNullOrEmpty(EmployeeCriteria.OrganizationId) || c.Organization.Id.ToLower().Contains(EmployeeCriteria.OrganizationId.ToLower()))
                 && (string.IsNullOrEmpty(EmployeeCriteria.Branch) || c.Branch.Name.ToLower().Contains(EmployeeCriteria.Branch.ToLower()))
                 && (string.IsNullOrEmpty(EmployeeCriteria.DepartmentId) || c.DepartmentId.ToLower().Contains(EmployeeCriteria.DepartmentId.ToLower()))
                 && (string.IsNullOrEmpty(EmployeeCriteria.Origin) || c.Origin.ToLower().Contains(EmployeeCriteria.Origin.ToLower()))
                 && ((EmployeeCriteria.IsActive == null) || (EmployeeCriteria.IsActive == c.IsActive))
                 && c.Id != null;

                empResponse.Employees = await _repository.GetPaginatedAsync<Employee>(filter, skip, take);

                empResponse.TotalCount = await _repository.CountAsync<Employee>(filter);

                return empResponse;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<Employee>> GetEmpFromDeptAsync(string departmentId) {
            try {

                return await _repository.GetAllAsync<Employee>(c => c.DepartmentId == departmentId);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<Employee>> GetEmployeesbyOriginAsync(string origin) {
            try {

                return await _repository.GetAllAsync<Employee>(c => c.Origin == origin);
            }
            catch (Exception ex) {

                throw ex;
            }

        }

        public async Task<CommonResponse> InsertEmployeeAsync(Employee employee) {
            CommonResponse res = new CommonResponse();
            try {
                var Exists = await _repository.GetOneAsync<Employee>(z => z.Mobile.ToLower() == employee.Mobile.ToLower() || z.Email.ToLower() == employee.Email.ToLower());
                if (Exists != null) {
                    res.IsSuccess = false;
                    if (Exists.Mobile.ToLower() == employee.Mobile.ToLower())
                        res.Message = "Mobile no already exists.!!!";
                    if (Exists.Email.ToLower() == employee.Email.ToLower())
                        res.Message = "Email id already exists.!!!";
                    return res;
                }

                await _repository.AddOneAsync(employee);

                if (!string.IsNullOrEmpty(employee.DepartmentId)) {
                    var deptPermission = await _permissionRepository.GetDeptPermissionAsync(employee.DepartmentId);
                    if (deptPermission != null && !string.IsNullOrEmpty(deptPermission.Id)) {

                        var empPermission = new EmpPermission();
                        empPermission.EmployeeId = employee.Id;
                        empPermission.EmployeeName = employee.FullName;
                        empPermission.Origin = employee.Origin;
                        empPermission.Menus = deptPermission.Menus;
                        empPermission.NavGroups = deptPermission.NavGroups;
                        empPermission.NavItems = deptPermission.NavItems;
                        empPermission.ReportGroups = deptPermission.ReportGroups;
                        empPermission.ReportItems = deptPermission.ReportItems;
                        empPermission.Actions = deptPermission.Actions;
                        empPermission.GridConfigs = deptPermission.GridConfigs;

                        await _permissionRepository.InsertEmpPermissionAsync(empPermission);
                    }
                }

                res.IsSuccess = true;
                res.Message = employee.Id;

                return res;
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.Message = "Something went wrong, Try again later.";
                res.ErrorMessage = ex.Message + " Inner ex: " + ex.InnerException?.Message;
                return res;
            }

        }

        public async Task<CommonResponse> UpdateEmployeeAsync(Employee employee) {
            CommonResponse res = new CommonResponse();
            try {
                var Exists = await _repository.GetOneAsync<Employee>(z => z.Id != employee.Id && (z.Mobile.ToLower() == employee.Mobile.ToLower() || z.Email.ToLower() == employee.Email.ToLower()));
                if (Exists != null) {
                    res.IsSuccess = false;
                    if (Exists.Mobile.ToLower() == employee.Mobile.ToLower())
                        res.Message = "Mobile no already exists.!!!";
                    if (Exists.Email.ToLower() == employee.Email.ToLower())
                        res.Message = "Email id already exists.!!!";
                    return res;
                }

                await _repository.UpdateOneAsync(employee);

                res.IsSuccess = true;
                res.Message = employee.Id;

                return res;
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.Message = "Something went wrong, Try again later.";
                res.ErrorMessage = ex.Message + " Inner ex: " + ex.InnerException?.Message;
                return res;
            }
        }

        public async Task<long> DeleteEmployeeAsync(string id) {
            try {

                return await _repository.DeleteOneAsync<Employee>(c => c.Id == id);
            }
            catch (Exception ex) {

                throw ex;
            }

        }

        public async Task<UserCredential> GetUserCredentialAsync(string id) {
            try {

                var result = await _repository.ProjectOneAsync<Employee, UserCredential>(c => c.Id == id,
                      j => new UserCredential {
                          Id = j.Id,
                          FullName = j.FullName,
                          Organization = j.Organization.Name,
                          OrganizationId = j.Organization.Id,
                          Department = j.Department,
                          DepartmentId = j.DepartmentId,
                          Branch = j.Branch.Name,
                          DeviceRFIDUrl = j.Branch.DeviceRFIDUrl,
                          Code = j.Code,
                          Origin = j.Origin,
                          IsAvailable = j.IsAvailable,
                          IsManager = j.IsManager
                      });

                return result;

            }
            catch (Exception ex) {

                throw ex;
            }

        }
    }
}