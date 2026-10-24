using CRM.Business.Organizations;
using CRM.DAL.BaseRepositories;
using CRM.DAL.ConfigRepositories;
using CRM.Entities.MongoEntities.Configuration;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.DAL.OrganizationRepositories {
    public class OrganizationRepository : IOrganizationRepository {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMongoRepository _repository;

        public OrganizationRepository(IMongoRepository repository, IPermissionRepository permissionRepository) {
            _repository = repository;
            _permissionRepository = permissionRepository;
        }

        public async Task<Organization> GetOrganizationAsync(string id) {
            try {

                return await _repository.GetOneAsync<Organization>(c => c.Id == id);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<Organization>> GetOrganizationByNameAsync(string name) {
            try {

                return await _repository.GetAllAsync<Organization>(c => c.Name.Contains(name));
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }

        public async Task<IList<Organization>> GetOrganizationsAsync() {
            try {

                return await _repository.GetAllAsync<Organization>(c => c.Id != null);
            }
            catch (Exception ex) {

                throw ex;
            }
            
        }

        public async Task<OrganizationResponse> GetOrganizationsAsync(OrganizationSearchCriteria OrganizationCriteria, int skip, int take) {
            try {
                var orgResponse = new OrganizationResponse();
                Expression<Func<Organization, bool>> filter = c => (string.IsNullOrEmpty(OrganizationCriteria.Name) || c.Name.ToLower().Contains(OrganizationCriteria.Name.ToLower()))
                 && (string.IsNullOrEmpty(OrganizationCriteria.BusinessType) || c.BusinessType.ToLower().Contains(OrganizationCriteria.BusinessType.ToLower()))
                 && (string.IsNullOrEmpty(OrganizationCriteria.OrganizationType) || c.OrganizationType.ToLower().Contains(OrganizationCriteria.OrganizationType.ToLower()))
                 && (string.IsNullOrEmpty(OrganizationCriteria.RegistrationNo) || c.RegistrationNo.ToLower().Contains(OrganizationCriteria.RegistrationNo.ToLower()))
                 && (string.IsNullOrEmpty(OrganizationCriteria.Country) || c.Address.Country.ToLower().Contains(OrganizationCriteria.Country.ToLower()))
                 && (string.IsNullOrEmpty(OrganizationCriteria.Email) || c.Email.ToLower().Contains(OrganizationCriteria.Email.ToLower()))
                 && c.Id != null;

                orgResponse.Organizations = await _repository.GetPaginatedAsync<Organization>(filter, skip, take);
                orgResponse.TotalCount = await _repository.CountAsync<Organization>(filter);

                return orgResponse;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<OrganizationDNorm>> GetOrganizationDNormsAsync() {
            try {

                var result = await _repository.ProjectManyAsync<Organization, OrganizationDNorm>(c => c.Id != null,
                   j => new OrganizationDNorm {
                       Id = j.Id,
                       Name = j.Name,
                       Person = j.Person,
                       PhoneNo = j.PhoneNo,
                       Email = j.Email
                   });

                return result;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<OrganizationDNorm>> GetOrganizationDNormsAsync(string name) {
            try {

                var result = await _repository.ProjectManyAsync<Organization, OrganizationDNorm>(c => c.Name.Contains(name),
                        j => new OrganizationDNorm {
                            Id = j.Id,
                            Name = j.Name,
                            Person = j.Person,
                            PhoneNo = j.PhoneNo,
                            Email = j.Email
                        });

                return result;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<BranchDNorm>> GetBranchDNormsAsync(string organizationId) {
            try {
                var dNormList = await _repository.ProjectOneAsync<Organization, List<BranchDNorm>>(c => c.Id == organizationId, c => c.Branches.Select(z => new BranchDNorm {
                    Name = z.Name,
                    Person = z.Person,
                    MobileNo = z.MobileNo,
                    Email = z.Email,
                    DeviceRFIDUrl = z.DeviceRFIDUrl
                }).ToList());

                return dNormList;
            }
            catch (Exception ex) {

                throw ex;
            }
          
        }

        public async Task<IList<Department>> GetDepartmentsAsync(string organizationId) {
            try {

                var departmentNames = await _repository.ProjectOneAsync<Organization, List<Department>>(c => c.Id == organizationId, c => c.Departments.ToList());

                return departmentNames;
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }

        public async Task<string> InsertOrganizationAsync(Organization organization) {
            try {

                await _repository.AddOneAsync(organization);

                return organization.Id;
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }

        public async Task<bool> ExistBranchAsync(string organizationId, Branch branch, bool IsInsert) {
            try {

                bool flag = false;

                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);

                if (flag)
                    flag = organization.Branches.Any(c => c.Name == branch.Name);
                else
                    flag = organization.Branches.Any(c => (c.Id != branch.Id && c.Name == branch.Name));

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }

        public async Task<bool> ExistDepartmentAsync(string organizationId, Department department, bool IsInsert) {
            try {

                bool flag = false;

                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);

                if (flag)
                    flag = organization.Departments.Any(c => c.Name == department.Name);
                else
                    flag = organization.Departments.Any(c => (c.Id != department.Id && c.Name == department.Name));

                return flag;

            }
            catch (Exception ex) {

                throw ex;
            }

        }

        public async Task<bool> InsertBranchAsync(string organizationId, Branch branch) {
            try {

                bool flag = false;

                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);
                if (organization != null && !string.IsNullOrEmpty(organization.Id) && !string.IsNullOrEmpty(branch.Name)) {
                    branch.Id = ObjectId.GenerateNewId().ToString();
                    var update = Builders<Organization>.Update.Push(c => c.Branches, branch);
                    flag = await _repository.UpdateOneAsync<Organization>(organization, update);
                }

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }

        public async Task<bool> InsertDepartmentAsync(string organizationId, Department department) {
            try {

                bool flag = false;

                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);
                if (organization != null && !string.IsNullOrEmpty(organization.Id) && !string.IsNullOrEmpty(department.Name)) {

                    department.Id = ObjectId.GenerateNewId().ToString();

                    var update = Builders<Organization>.Update.Push(c => c.Departments, department);
                    flag = await _repository.UpdateOneAsync<Organization>(organization, update);
                    if (flag) {

                        var originPermission = await _permissionRepository.GetOriginPermissionsAsync(department.Origin);
                        if (originPermission != null && !string.IsNullOrEmpty(originPermission.Id)) {

                            var depPermission = new DeptPermission();
                            depPermission.DepartmentId = department.Id;
                            depPermission.Department = department.Name;
                            depPermission.Origin = department.Origin;
                            depPermission.Menus = originPermission.Menus;
                            depPermission.NavGroups = originPermission.NavGroups;
                            depPermission.NavItems = originPermission.NavItems;
                            depPermission.ReportGroups = originPermission.ReportGroups;
                            depPermission.ReportItems = originPermission.ReportItems;
                            depPermission.Actions = originPermission.Actions;
                            depPermission.GridConfigs = originPermission.GridConfigs;

                            var deptPermId = await _permissionRepository.InsertDeptPermissionAsync(depPermission);
                        }
                    }

                }

                return flag;

            }
            catch (Exception ex) {

                throw ex;
            }
           
        }


        public async Task<bool> UpdateOrganizationAsync(Organization organization) {
            try {

                var result = await _repository.UpdateOneAsync<Organization>(organization);

                return result;
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }

        public async Task<bool> UpdateOrganizationBranchAsync(string organizationId, Branch branch) {
            try {

                var remove = Builders<Organization>.Update.PullFilter(c => c.Branches, f => f.Id == branch.Id);
                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);

                var flag = await _repository.UpdateOneAsync<Organization>(organization, remove);
                if (flag) {
                    var update = Builders<Organization>.Update.Push(c => c.Branches, branch);
                    flag = await _repository.UpdateOneAsync<Organization>(organization, update);
                }

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
            
        }

        public async Task<bool> UpdateOrganizationDepartmentAsync(string organizationId, Department department) {
            try {

                var remove = Builders<Organization>.Update.PullFilter(c => c.Departments, f => f.Id == department.Id);
                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);

                var flag = await _repository.UpdateOneAsync<Organization>(organization, remove);
                if (flag) {
                    var update = Builders<Organization>.Update.Push(c => c.Departments, department);
                    flag = await _repository.UpdateOneAsync<Organization>(organization, update);
                }

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
          
        }

        public async Task<long> DeleteOrganizationAsync(string id) {
            try {

                return await _repository.DeleteOneAsync<Organization>(c => c.Id == id);
            }
            catch (Exception ex) {

                throw ex;
            }
            
        }

        public async Task<bool> DeleteBranchAsync(string organizationId, string branchId) {
            try {

                bool flag = false;

                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);
                if (organization != null && !string.IsNullOrEmpty(organization.Id)) {
                    var remove = Builders<Organization>.Update.PullFilter(c => c.Branches, f => f.Id == branchId);
                    flag = await _repository.UpdateOneAsync<Organization>(organization, remove);
                }

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
            
        }

        public async Task<bool> DeleteDepartmentAsync(string organizationId, string departmentId) {
            try {

                bool flag = false;

                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == organizationId);
                if (organization != null && !string.IsNullOrEmpty(organization.Id)) {
                    var remove = Builders<Organization>.Update.PullFilter(c => c.Departments, f => f.Id == departmentId);
                    flag = await _repository.UpdateOneAsync<Organization>(organization, remove);
                }

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
          
        }

        public async Task<Address> GetOrgAddressByEmployeeAsync(string employeeId) {
            try {

                var emp = await _repository.GetOneAsync<Employee>(c => c.Id == employeeId);
                var organization = await _repository.GetOneAsync<Organization>(c => c.Id == emp.Organization.Id);

                if (emp.Branch != null && !string.IsNullOrEmpty(emp.Branch.Name) && emp.Branch.Name.Length > 0) {
                    Branch branch = organization.Branches.FirstOrDefault(z => z.Name == emp.Branch.Name);
                    if (branch != null)
                        return branch.Address;
                    else
                        return organization.Address;
                }
                else
                    return organization.Address;
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }
    }
}