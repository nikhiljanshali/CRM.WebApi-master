using CRM.Business.Common;
using CRM.Business.Inventory;
using CRM.DAL.BaseRepositories;
using CRM.DAL.CommonRepositories;
using CRM.DAL.PricingRepositories;
using CRM.Entities.MongoEntities.Inventory;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DAL.StockOnHandRepositories {
    public class StockOnHandRepository : IStockOnHandRepository {
        private readonly IMongoRepository _repository;
        private readonly IPriceRequestRepository _pricingRequestRepository;
        private readonly Mapping _mapping;
        private readonly Filters _filters;

        public StockOnHandRepository(IMongoRepository repository, IPriceRequestRepository pricingRequestRepository) {
            _repository = repository;
            _pricingRequestRepository = pricingRequestRepository;
            _mapping = new Mapping(_repository);
            _filters = new Filters(_repository);
        }

        #region Get
        public async Task<InventoryItems> GetStockOnHandAsync(string id) {
            try {
                return await _repository.GetOneAsync<InventoryItems>(c => c.Id == id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<InventoryItems>> GetStockOnHandAsync() {
            try {
                return await _repository.GetAllAsync<InventoryItems>(c => c.Id != null);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<StockOnHandResponse> GetStockOnHandAsync(StockOnHandSearchCriteria StockOnHandSearchCriteria, int skip, int take) {
            try {
                var invResponse = new StockOnHandResponse();
                var allInv = await _repository.GetAllAsync<InventoryItems>(c => c.Identity.Id == StockOnHandSearchCriteria.EmployeeId);
                var filterInv = allInv.Where(c => FilterData(c, StockOnHandSearchCriteria)).ToList();
                invResponse.TotalCount = filterInv.Count();
                invResponse.Inventories = filterInv.Skip(skip).Take(take).ToList();

                return invResponse;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<IList<ConsignmentInvoice>> GetConsignmentInvoicesAsync() {
            try {
                return await _repository.GetAllAsync<ConsignmentInvoice>(c => c.Id != null);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InventoryItems> GetStockOnHandByStoneIdAsync(string stoneId) {
            try {
                return await _repository.GetOneAsync<InventoryItems>(c => c.StoneId.ToLower() == stoneId.ToLower());
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region Insert
        public async Task<CommonResponse> InsertConsignmentInvoice(ConsignmentInvoice consignmentInvoice) {
            try {
                CommonResponse res = new CommonResponse();
                await _repository.AddOneAsync(consignmentInvoice);

                if (consignmentInvoice.ConsignmentInvoiceItems != null && consignmentInvoice.ConsignmentInvoiceItems.Count > 0) {
                    var filter = Builders<InventoryItems>.Filter.In(c => c.StoneId, consignmentInvoice.ConsignmentInvoiceItems.Select(v => v.StoneId));
                    var update = Builders<InventoryItems>.
                        Update.Set(c => c.Status, "Transit")
                                     .Set(c => c.IsMemo, true)
                                    .Set(c => c.UpdatedAt, System.DateTime.UtcNow);

                    await _repository.UpdateManyAsync<InventoryItems>(filter, update);
                }
                res.IsSuccess = true;
                res.Message = consignmentInvoice.Id;
                return res;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region Update
        public async Task<long> UpdatePricingRequestAsync(IList<InventoryItems> inventoryItems) {
            try {
                if (inventoryItems != null && inventoryItems.Count > 0) {
                    var filter = Builders<InventoryItems>.Filter.In(c => c.StoneId, inventoryItems.Select(v => v.StoneId));
                    var update = Builders<InventoryItems>.
                        Update.Set(c => c.IsPricingRequest, inventoryItems[0].IsPricingRequest)
                                     .Set(c => c.UpdatedBy, inventoryItems[0].UpdatedBy)
                                    .Set(c => c.UpdatedAt, DateTime.UtcNow);

                    //Add in Pricing Request
                    var req = await _pricingRequestRepository.InsertPricingRequestFromInvItems(inventoryItems, "StockOnHand");
                    if (req)
                        return await _repository.UpdateManyAsync(filter, update);
                }
                return 0;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<string>> UpdateStockAsync(IList<InventoryItems> inventoryItems) {
            try {
                IList<string> Ids = new List<string>();
                if (inventoryItems != null && inventoryItems.Count > 0) {
                    List<StockOnHandHistory> stones = new List<StockOnHandHistory>();

                    foreach (var items in inventoryItems) {
                        StockOnHandHistory stone = _mapping.MappingInvToStockOnHandHistory(items);

                        stones.Add(stone);
                        Ids.Add(items.StoneId);
                    }

                    if (inventoryItems != null && inventoryItems.Count > 0) {
                        var filter = Builders<InventoryItems>.Filter.In(c => c.StoneId, inventoryItems.Select(v => v.StoneId));
                        var update = Builders<InventoryItems>.
                            Update.Set(c => c.Identity.Id, inventoryItems[0].Identity.Id)
                                         .Set(c => c.Identity.Name, inventoryItems[0].Identity.Name)
                                         .Set(c => c.Identity.Type, inventoryItems[0].Identity.Type)
                                        .Set(c => c.UpdatedAt, System.DateTime.UtcNow);

                        await _repository.AddManyAsync(stones);
                        await _repository.UpdateManyAsync<InventoryItems>(filter, update);
                    }
                }
                return Ids;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<long> UpdateInventoryMarketSheetAsync(IList<InventoryItems> inventoryItems) {
            try {
                long flag = 0;

                if (inventoryItems != null && inventoryItems.Count > 0) {
                    var filter = Builders<InventoryItems>.Filter.In(c => c.StoneId, inventoryItems.Select(v => v.StoneId));
                    var update = Builders<InventoryItems>.Update.Set(c => c.MarketSheetDate, System.DateTime.UtcNow);

                    flag = await _repository.UpdateManyAsync<InventoryItems>(filter, update);
                }
                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region AddOn Functions
        private bool FilterData(InventoryItems data, StockOnHandSearchCriteria filter) {

            List<string> stoneIds = new List<string>();
            if (filter.StoneId != null) {
                if (filter.StoneId.Contains(","))
                    stoneIds = filter.StoneId.Split(",").ToList();
                else if (filter.StoneId.Contains(" "))
                    stoneIds = filter.StoneId.Split(" ").ToList();
                else
                    stoneIds.Add(filter.StoneId);
            }


            return (filter.MinSize == null || filter.MaxSize == null || (filter.MinSize <= data.Weight && filter.MaxSize >= data.Weight))
                && (filter.From == null || filter.To == null || ((filter.From != null && filter.To != null) && filter.From?.Date <= data.CreatedDate.Date && filter.To?.Date >= data.CreatedDate.Date))
                //&& CheckCommonString(data.StoneOrg.BranchName, filter.Branch)
                //&& CheckCommonString(data.StoneOrg.DeptId, filter.DeptId)
                //&& CheckCommonString(data.Identity.Id, filter.EmpId)
                && _filters.CompareArrayString(data.StoneId, stoneIds)
                && _filters.CheckCommonBoolen(data.IsHold, filter.IsHold)
                //&& CompareArrayString(data.Location, filter.Location)
                //&& CompareArrayString(data.Status, filter.Status)
                && _filters.CompareArrayString(data.Shape, filter.Shape)
                && _filters.CompareArrayString(data.Color, filter.Color)
                && _filters.CompareArrayString(data.Clarity, filter.Clarity)
                && _filters.CompareArrayString(data.Fluorescence, filter.Fluorescence)
                && _filters.CompareArrayString(data.Cut, filter.Cut)
                && _filters.CompareArrayString(data.Polish, filter.Polish)
                && _filters.CompareArrayString(data.Symmetry, filter.Symmetry);
        } 
        #endregion

    }
}