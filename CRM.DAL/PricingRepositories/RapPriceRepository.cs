using CRM.Business.Pricing;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.PricingRepositories {
    public class RapPriceRepository : IRapPriceRepository {
        private readonly IMongoRepository _repository;

        public RapPriceRepository(IMongoRepository repository) {
            _repository = repository;
        }

        #region Get
        public async Task<List<RapPrice>> GetAllRapPriceAsync() {
            return await _repository.GetAllAsync<RapPrice>(z => z.Id != null);
        }

        public async Task<RapPrice> GetRapPriceAsync(RapPriceRequest req) {
            try {
                return await _repository.GetOneAsync<RapPrice>(z => z.Shape.ToLower() == req.Shape.ToLower() &&
                z.MinSize <= req.Weight && z.MaxSize >= req.Weight &&
                z.Color.ToLower() == req.Color.ToLower() &&
                z.Clarity.ToLower() == req.Clarity.ToLower());
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region Update
        public async Task<bool> UpdateRapPrice(IList<RapPrice> data) {
            try {
                var shapes = data.Select(u => u.Shape.ToLower()).Distinct().ToList();

                var allData = new List<RapPrice>();
                shapes.ForEach(z => {
                    var all = _repository.GetAll<RapPrice>(a => a.Shape.ToLower() == z);
                    allData.AddRange(all);
                });

                var flag = await _repository.DeleteManyAsync(allData);
                if (flag > 0)
                    await _repository.AddManyAsync(data);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        } 
        #endregion
    }
}
