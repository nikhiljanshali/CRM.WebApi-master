using CRM.Business.Customers;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Registration;
using CRM.Entities.MongoEntities.Registration.DNorm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CustomerRepositories {
    public interface ICustomerVerificationRepository {
        Task<IList<RegisterCustomer>> GetAllCustomersAsync(string status);
        Task<CustomerVerificationResponse> GetCustomersAsync(CustomerVerificationSearchCriteria CustomerVerificationCriteria, int skip, int take);
        Task<string> AcceptOrRejectCustomersAsync(Customer customer,bool isAccept);
        Task<bool> ExistCustomerAsync(Customer customer);
        Task<IList<SellerDNorm>> GetSupplierDNormsAsync();
    }
}