using CRM.Business.Organizations;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public interface IOrganizationRepository {
        Task<Organization> GetOrganizationAsync(string id);
        Task<IList<Organization>> GetOrganizationByNameAsync(string name);
        Task<IList<Organization>> GetOrganizationsAsync();
        Task<OrganizationResponse> GetOrganizationsAsync(OrganizationSearchCriteria OrganizationCriteria, int skip, int take);
        Task<IList<OrganizationDNorm>> GetOrganizationDNormsAsync();
        Task<IList<OrganizationDNorm>> GetOrganizationDNormsAsync(string name);
        Task<IList<BranchDNorm>> GetBranchDNormsAsync(string organizationId);
        Task<IList<Department>> GetDepartmentsAsync(string organizationId);
        Task<Address> GetOrgAddressByEmployeeAsync(string employeeId);
        Task<bool> ExistBranchAsync(string organizationId, Branch branch, bool IsInsert);
        Task<bool> ExistDepartmentAsync(string organizationId, Department department, bool IsInsert);

        Task<string> InsertOrganizationAsync(Organization organization);
        Task<bool> InsertBranchAsync(string organizationId, Branch branch);
        Task<bool> InsertDepartmentAsync(string organizationId, Department department);

        Task<bool> UpdateOrganizationAsync(Organization organization);
        Task<bool> UpdateOrganizationBranchAsync(string organizationId, Branch branch);
        Task<bool> UpdateOrganizationDepartmentAsync(string organizationId, Department department);

        Task<long> DeleteOrganizationAsync(string id);
        Task<bool> DeleteBranchAsync(string organizationId, string branchId);
        Task<bool> DeleteDepartmentAsync(string organizationId, string departmentId);
    }
}
