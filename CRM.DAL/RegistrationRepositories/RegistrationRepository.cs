using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Registration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.RegistrationRepositories {
    public class RegistrationRepository : IRegistrationRepository {
        private readonly IMongoRepository _repository;

        public RegistrationRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<IList<RegisterCustomer>> GetRegisterCustomersAsync() {
            try {
                return await _repository.GetAllAsync<RegisterCustomer>(c => c.Id != null);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<RegisterCustomer>> GetRegisterCustomersAsync(string status) {
            try {
                return await _repository.GetAllAsync<RegisterCustomer>(c => c.Status.ToLower() == StatusType.InProgress.ToString().ToLower());
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<RegisterCustomer>> GetRegisterCustomersAsync( int skip, int take) {
            try {
                return await _repository.GetPaginatedAsync<RegisterCustomer>(c => c.Status.ToLower() != StatusType.InProgress.ToString().ToLower(), skip, take);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<IList<RegisterCustomer>> GetRegisterCustomersAsync(string status, int skip, int take) {
            try {
                return await _repository.GetPaginatedAsync<RegisterCustomer>(c => c.Status.ToLower() == StatusType.InProgress.ToString().ToLower(), skip, take);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<string> InsetRegisterCustomerAsync(RegisterCustomer registerCustomer) {
            try {
                await _repository.AddOneAsync<RegisterCustomer>(registerCustomer);
                return registerCustomer.Id;
            }
            catch (Exception ex) {

                throw ex;
            }

        }

        public async Task<bool> ExistInProgressCustomerAsync(RegisterCustomer registerCustomer) {
            try {
                bool flag = false;

                RegisterCustomer customerExist = await _repository.GetOneAsync<RegisterCustomer>(c =>( c.Email.ToLower() == registerCustomer.Email.ToLower().Trim() || c.PrimaryMobile.ToLower() == registerCustomer.PrimaryMobile.ToLower().Trim() || c.CompanyName.ToLower() == registerCustomer.CompanyName.ToLower().Trim()) && (c.Status.ToLower() == StatusType.InProgress.ToString().ToLower() || c.Status.ToLower() == StatusType.Rejected.ToString().ToLower()));
                if (customerExist != null)
                    flag = true;
                else
                    flag = false;
                
                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
          
        }

        public async Task<bool> ExistAcceptedCustomerAsync(RegisterCustomer registerCustomer) {
            try {
                bool flag = false;

                RegisterCustomer customerExist = await _repository.GetOneAsync<RegisterCustomer>(c => (c.Email.ToLower() == registerCustomer.Email.ToLower().Trim() || c.PrimaryMobile.ToLower() == registerCustomer.PrimaryMobile.ToLower().Trim() || c.CompanyName.ToLower() == registerCustomer.CompanyName.ToLower().Trim()) && (c.Status == StatusType.Accepted.ToString().ToLower()));
                if (customerExist != null)
                    flag = true;
                else
                    flag = false;

                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }

        }

        public async Task<bool> UpdateRegisterCustomerAsync(RegisterCustomer registerCustomer) {
            try {
                return await _repository.UpdateOneAsync<RegisterCustomer>(registerCustomer);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<long> DeleteRegisterCustomerAsync(string id) {
            try {
                return await _repository.DeleteOneAsync<RegisterCustomer>(c => c.Id == id);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

    }
}