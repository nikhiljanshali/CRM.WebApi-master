using CRM.Business.Customers;
using CRM.Core.Common;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Registration;
using CRM.Entities.MongoEntities.Registration.DNorm;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CustomerRepositories {
    public class CustomerVerificationRepository : ICustomerVerificationRepository {
        private readonly IMongoRepository _repository;

        public CustomerVerificationRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<IList<RegisterCustomer>> GetAllCustomersAsync(string status) {
            try {
                return await _repository.GetAllAsync<RegisterCustomer>(c => c.Status.ToLower() == status.ToLower());
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<CustomerVerificationResponse> GetCustomersAsync(CustomerVerificationSearchCriteria CustomerVerificationCriteria, int skip, int take) {
            try {
                var custResponse = new CustomerVerificationResponse();
                Expression<Func<RegisterCustomer, bool>> filter = c => (string.IsNullOrEmpty(CustomerVerificationCriteria.CompanyName) || c.CompanyName.ToLower().Contains(CustomerVerificationCriteria.CompanyName.ToLower()))
                 && (string.IsNullOrEmpty(CustomerVerificationCriteria.Country) || c.Address.Country.ToLower().Contains(CustomerVerificationCriteria.Country.ToLower()))
                 && (string.IsNullOrEmpty(CustomerVerificationCriteria.Email) || c.Email.ToLower().Contains(CustomerVerificationCriteria.Email.ToLower()))
                 && (string.IsNullOrEmpty(CustomerVerificationCriteria.MobileNo) || c.PrimaryMobile.ToLower().Contains(CustomerVerificationCriteria.MobileNo.ToLower()))
                 && ((c.Status.ToLower() == StatusType.InProgress.ToString().ToLower()) || (c.Status.ToLower() == StatusType.Rejected.ToString().ToLower()))
                 && c.Id != null;

                custResponse.Customers = await _repository.GetPaginatedAsync<RegisterCustomer>(filter, skip, take);
                custResponse.TotalCount = await _repository.CountAsync<RegisterCustomer>(filter);

                return custResponse;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<string> AcceptOrRejectCustomersAsync(Customer customer,bool isAccept) {
            try {
                bool result = false;
                string id = "";
                RegisterCustomer isExistCustomer = await _repository.GetOneAsync<RegisterCustomer>(c => c.Email == customer.Email);
                if (isExistCustomer != null && !string.IsNullOrEmpty(isExistCustomer.Id) && !string.IsNullOrEmpty(isExistCustomer.Email)){
                    if (isAccept) {
                        isExistCustomer.Status = StatusType.Accepted.ToString();
                    }
                    else {
                        isExistCustomer.Status = StatusType.Rejected.ToString();
                    }

                    result = await _repository.UpdateOneAsync<RegisterCustomer>(isExistCustomer);
                    if (result) {
                        if (isAccept) {
                           await _repository.AddOneAsync(customer);
                            id = customer.Id;
                        }
                        else {
                            id = "Rejected";
                        }
                    }
                    else {
                        id = "";
                    }
                }
               return id;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<bool> ExistCustomerAsync(Customer customer) {
            try {
                bool flag = false;

                Customer customerExist = await _repository.GetOneAsync<Customer>(c => (c.Email.ToLower() == customer.Email.ToLower().Trim() || c.Mobile1.ToLower() == customer.Mobile1.ToLower().Trim() || c.CompanyName.ToLower() == customer.CompanyName.ToLower().Trim()));
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

        public async Task<IList<SellerDNorm>> GetSupplierDNormsAsync() {
            try {
                var result = await _repository.ProjectManyAsync<Employee, SellerDNorm>(c => c.Id != null && c.Origin.ToLower() == OriginType.Seller.ToString().ToLower(),
                j => new SellerDNorm {
                    Id = j.Id,
                    Name = j.FullName,
                    Mobile = j.Mobile,
                    Email = j.Email
                });

                return result;
            }
            catch (Exception ex) {

                throw ex;
            }
        }
    }
}
