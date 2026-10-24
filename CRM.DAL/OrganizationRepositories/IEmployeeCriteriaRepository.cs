using CRM.Business.Common;
using CRM.Entities.MongoEntities.Organization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public interface IEmployeeCriteriaRepository {
        Task<EmployeeCriteria> GetEmployeeCriteriaAsync(string id);
        Task<IList<EmployeeCriteria>> GetEmployeeCriteriaByEmployeeAsync(string id);
        Task<IList<EmployeeCriteria>> GetEmployeeCriteriaAsync();
        Task<string> InsertEmployeeCriteriaAsync(EmployeeCriteria employeeCriteria);
        Task<bool> UpdateEmployeeCriteriaAsync(EmployeeCriteria employeeCriteria);
        Task<long> DeleteEmployeeCriteriaAsync(string id);
    }
}
