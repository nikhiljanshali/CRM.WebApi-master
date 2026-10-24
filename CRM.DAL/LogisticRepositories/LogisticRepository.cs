using CRM.Business.Logistics;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.LogisticCollection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.LogisticRepositories {
    public class LogisticRepository : ILogisticRepository {
        private readonly IMongoRepository _repository;

        public LogisticRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<Logistic> GetLogisticAsync(string id) {
            try {
                return await _repository.GetOneAsync<Logistic>(c => c.Id == id);
            }
            catch (Exception ex) {

                throw ex;
            }
           
        }

        public async Task<IList<Logistic>> GetLogisticsAsync() {
            try {
                return await _repository.GetAllAsync<Logistic>(c => c.Id != null);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<LogisticResponse> GetLogisticsAsync(LogisticSearchCriteria logisticCriteria, int skip, int take) {
            try {
                var logisticResponse = new LogisticResponse();
                Expression<Func<Logistic, bool>> filter = c => (string.IsNullOrEmpty(logisticCriteria.Name) || c.Name.ToLower().Contains(logisticCriteria.Name.ToLower()))
                && (string.IsNullOrEmpty(logisticCriteria.Country) || c.Address.Country.ToLower().Contains(logisticCriteria.Country.ToLower()))
                && (string.IsNullOrEmpty(logisticCriteria.State) || c.Address.State.ToLower().Contains(logisticCriteria.State.ToLower()))
                && (string.IsNullOrEmpty(logisticCriteria.City) || c.Address.City.ToLower().Contains(logisticCriteria.City.ToLower()))
                && c.Id != null;

                logisticResponse.Logistics = await _repository.GetPaginatedAsync<Logistic>(filter, skip, take);
                logisticResponse.TotalCount = await _repository.CountAsync<Logistic>(filter);

                return logisticResponse;
            }
            catch (Exception ex) {

                throw ex;
            }
        }
    
        public async Task<string> InsertLogisticAsync(Logistic logistic) {
            try {
                await _repository.AddOneAsync(logistic);

                return logistic.Id;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<bool> UpdateLogisticAsync(Logistic logistic) {
            try {
                var result = await _repository.UpdateOneAsync<Logistic>(logistic);

                return result;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<long> DeleteLogisticAsync(string id) {
            try {
                return await _repository.DeleteOneAsync<Logistic>(c => c.Id == id);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

    }
}
