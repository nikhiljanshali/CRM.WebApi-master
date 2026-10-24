using CRM.Business.Common;
using CRM.Business.Employees;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public interface IEmployeeRepository {
        Task<Employee> GetEmployeeAsync(string id);
        Task<IList<Employee>> GetEmployeesAsync();
        Task<IList<Employee>> GetEmployeesAsync(bool isActive);
        Task<IList<Employee>> GetEmployeesAsync(int skip, int take);
        Task<EmployeeResponse> GetEmployeesAsync(EmployeeSearchCriteria EmployeeCriteria, int skip, int take);
        Task<IList<Employee>> GetEmpFromDeptAsync(string id);
        Task<IList<Employee>> GetEmployeesbyOriginAsync(string origin);
        Task<CommonResponse> InsertEmployeeAsync(Employee employee);
        Task<CommonResponse> UpdateEmployeeAsync(Employee employee);
        Task<long> DeleteEmployeeAsync(string id);
        Task<UserCredential> GetUserCredentialAsync(string id);
    }
}
