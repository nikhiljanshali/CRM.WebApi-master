using CRM.Business.Common;
using CRM.Business.SystemUsers;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.SystemUserNameSpace;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.SystemUserRepositories {
    public class SystemUserRepository : ISystemUserRepository {
        private readonly IMongoRepository _repository;

        public SystemUserRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<SystemUser> GetSystemUserAsync(string id) {
            return await _repository.GetOneAsync<SystemUser>(c => c.Id == id);
        }

        public async Task<IList<SystemUser>> GetSystemUsersAsync() {
            return await _repository.GetAllAsync<SystemUser>(c => c.Id != null);
        }
        
        public async Task<IList<SystemUser>> GetSystemUsersbyOriginAsync(string origin) {
            return await _repository.GetAllAsync<SystemUser>(c => c.Origin == origin);
        }

        public async Task<IList<SystemUser>> GetSystemUsersAsync(bool isActive) {
            return await _repository.GetAllAsync<SystemUser>(c => c.IsActive == isActive);
        }

        public async Task<IList<SystemUser>> GetSystemUsersAsync(int skip, int take) {
            return await _repository.GetPaginatedAsync<SystemUser>(c => c.Id != null, skip, take);
        }

        public async Task<SystemUserResponse> GetSystemUsersAsync(SystemUserSearchCriteria SystemUserCriteria, int skip, int take) {

            var systemUserResponse = new SystemUserResponse();

            Expression<Func<SystemUser, bool>> filter = c =>
            (string.IsNullOrEmpty(SystemUserCriteria.FirstName) || c.Name.FirstName.ToLower().Contains(SystemUserCriteria.FirstName.ToLower()))
             && (string.IsNullOrEmpty(SystemUserCriteria.LastName) || c.Name.LastName.ToLower().Contains(SystemUserCriteria.LastName.ToLower()))
             && (string.IsNullOrEmpty(SystemUserCriteria.Origin) || c.Origin.ToLower().Contains(SystemUserCriteria.Origin.ToLower()))
             && ((SystemUserCriteria.IsActive == null) || (SystemUserCriteria.IsActive == c.IsActive))
             && c.Id != null;

            systemUserResponse.SystemUsers = await _repository.GetPaginatedAsync<SystemUser>(filter, skip, take);

            systemUserResponse.TotalCount = await _repository.CountAsync<SystemUser>(filter);

            return systemUserResponse;
        }

        public async Task<CommonResponse> InsertSystemUserAsync(SystemUser systemUser) {
            CommonResponse res = new CommonResponse();
            try {
                var Exists = await _repository.GetOneAsync<SystemUser>(z => z.Mobile.ToLower() == systemUser.Mobile.ToLower() || z.Email.ToLower() == systemUser.Email.ToLower());
                if (Exists != null) {
                    res.IsSuccess = false;
                    if (Exists.Mobile.ToLower() == systemUser.Mobile.ToLower())
                        res.Message = "Mobile no already exists.!!!";
                    if (Exists.Email.ToLower() == systemUser.Email.ToLower())
                        res.Message = "Email id already exists.!!!";
                    return res;
                }

                await _repository.AddOneAsync(systemUser);
                res.IsSuccess = true;
                res.Message = systemUser.Id;

                return res;
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.Message = "Something went wrong, Try again later.";
                res.ErrorMessage = ex.Message + " Inner ex: " + ex.InnerException?.Message;
                return res;
            }
        }

        public async Task<CommonResponse> UpdateSystemUserAsync(SystemUser systemUser) {
            CommonResponse res = new CommonResponse();
            try {
                var Exists = await _repository.GetOneAsync<SystemUser>(z => z.Id != systemUser.Id && (z.Mobile.ToLower() == systemUser.Mobile.ToLower() || z.Email.ToLower() == systemUser.Email.ToLower()));
                if (Exists != null) {
                    res.IsSuccess = false;
                    if (Exists.Mobile.ToLower() == systemUser.Mobile.ToLower())
                        res.Message = "Mobile no already exists.!!!";
                    if (Exists.Email.ToLower() == systemUser.Email.ToLower())
                        res.Message = "Email id already exists.!!!";
                    return res;
                }

                await _repository.UpdateOneAsync(systemUser);

                res.IsSuccess = true;
                res.Message = systemUser.Id;

                return res;
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.Message = "Something went wrong, Try again later.";
                res.ErrorMessage = ex.Message + " Inner ex: " + ex.InnerException?.Message;
                return res;
            }
        }

        public async Task<long> DeleteSystemUserAsync(string id) {
            return await _repository.DeleteOneAsync<SystemUser>(c => c.Id == id);
        }
              
    }
}