using CRM.Entities.MongoEntities.Registration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.RegistrationRepositories {
    /// <summary>
    /// Registeration of customer repository
    /// </summary>
    public interface IRegistrationRepository {  

        /// <summary>
        /// Return list of customer registration requests
        /// </summary>
        /// <returns>List of CustomerRegistration</returns>
        Task<IList<RegisterCustomer>> GetRegisterCustomersAsync();

        /// <summary>
        /// Return list of customer registration requests
        /// </summary>
        /// <param name="status">Status for accepted or rejected.</param>
        /// <returns>List of RegisterCustomer</returns>
        Task<IList<RegisterCustomer>> GetRegisterCustomersAsync(string status);

        /// <summary>
        /// Return list of customer registration request
        /// </summary>
        /// <param name="skip">Skipped document</param>
        /// <param name="take">Count of document</param>
        /// <returns>List of RegisterCustomer</returns>
        Task<IList<RegisterCustomer>> GetRegisterCustomersAsync(int skip, int take);

        /// <summary>
        /// Return list of customer registration request
        /// </summary>
        /// <param name="status">Status for accepted or rejected.</param>
        /// <param name="skip">Skipped document</param>
        /// <param name="take">Count of document</param>
        /// <returns>List of RegisterCustomer</returns>
        Task<IList<RegisterCustomer>> GetRegisterCustomersAsync(string status, int skip, int take);

        /// <summary>
        /// Register customer 
        /// </summary>
        /// <param name="RegisterCustomer">RegisterCustomer document</param>
        /// <returns>Sucessfull if document added in db</returns>
        Task<bool> ExistInProgressCustomerAsync(RegisterCustomer registerCustomer);
        Task<bool> ExistAcceptedCustomerAsync(RegisterCustomer registerCustomer);
        Task<string> InsetRegisterCustomerAsync(RegisterCustomer registerCustomer);

        /// <summary>
        /// Update customer registration request.
        /// </summary>
        /// <param name="RegisterCustomer">RegisterCustomer document</param>
        /// <returns>True | False</returns>
        Task<bool> UpdateRegisterCustomerAsync(RegisterCustomer registerCustomer);

        /// <summary>
        /// Delete customer registration
        /// </summary>
        /// <param name="id">Id of document</param>
        /// <returns>Count of deleted documents.</returns>
        Task<long> DeleteRegisterCustomerAsync(string id);
    }
}
