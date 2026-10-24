using CRM.Business.Inventory;
using CRM.Business.Pricing;
using CRM.DAL.BaseRepositories;
using CRM.DAL.CommonRepositories;
using CRM.DAL.InventoryRepositories;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Pricing;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DAL.PricingRepositories {
    public class PriceRequestRepository : IPriceRequestRepository {
        private readonly IMongoRepository _repository;
        private readonly IInventoryRepository _invRepository;
        private readonly Mapping _mapping;
        private readonly Filters _filters;

        public PriceRequestRepository(IMongoRepository repository, IInventoryRepository invRepository) {
            _repository = repository;
            _invRepository = invRepository;
            _mapping = new Mapping(_repository);
            _filters = new Filters(_repository);
        }

        #region Get
        public async Task<IList<TempPricing>> GetTempPricingDataAsync(string empId) {
            try {
                return await _repository.GetAllAsync<TempPricing>(c => c.Identity.Id == empId);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<PriceReqResponse> GetPriceRequestDataAsync(PriceRequestApiModel req) {
            try {
                var res = new PriceReqResponse();

                List<PricingRequest> pricingsRequest = new List<PricingRequest>();
                if (req.Type == "Inv") {
                    //Get All Temp
                    IList<TempPricing> tempPricings = await GetTempPricingDataAsync(req.EmployeeId);

                    //Get All Inv
                    var allInv = await _repository.GetAllAsync<InventoryItems>(c => c.IsPricingRequest == false);

                    //Apply Filter By Emp Slot
                    var filterInv = allInv.Where(c => FilterData(c, req.Slots)).ToList();

                    //Filter By Basic Inv
                    if (req.InvFilter != null && req.InvFilter != new InventorySearchCriteria())
                        filterInv = filterInv.Where(c => _filters.FilterInvData(c, req.InvFilter)).ToList();

                    //Get Temp Data
                    tempPricings = GetTempPricingFromInv(filterInv, tempPricings.ToList());

                    //Convert to Pricing Request
                    tempPricings.ToList().ForEach(z => { pricingsRequest.Add(_mapping.MappingTempPricingToPricingRequest(z, filterInv.FirstOrDefault(a => a.StoneId == z.StoneId).Status)); });
                }
                else if (req.Type == "Pricing") {
                    //Get All Pricing Data
                    pricingsRequest = await _repository.GetAllAsync<PricingRequest>(c => c.Id != null);

                    //Filter By Basic Inv
                    if (pricingsRequest != null && req.InvFilter != null && req.InvFilter != new InventorySearchCriteria())
                        pricingsRequest = pricingsRequest.Where(c => _filters.FilterTempPricingData(c, req.InvFilter)).ToList();
                }
                else {
                    //Get All Temp
                    IList<TempPricing> tempPricings = await GetTempPricingDataAsync(req.EmployeeId);

                    //Filter By Basic Inv
                    if (tempPricings != null && req.InvFilter != null && req.InvFilter != new InventorySearchCriteria())
                        tempPricings = tempPricings.Where(c => _filters.FilterTempPricingData(c, req.InvFilter)).ToList();

                    //Convert to Pricing Request
                    var stoneIds = tempPricings.Select(z => z.StoneId);
                    var allInv = await _repository.GetAllAsync<InventoryItems>(c => stoneIds.Contains(c.StoneId));
                    tempPricings.ToList().ForEach(z => { pricingsRequest.Add(_mapping.MappingTempPricingToPricingRequest(z, allInv.FirstOrDefault(a => a.StoneId == z.StoneId).Status)); });
                }

                res.TotalCount = pricingsRequest.Count();
                res.TotalWeight = pricingsRequest.Sum(z => z.Weight);
                res.TotalNetAmount = pricingsRequest.Sum(z => z.BasePrice.NetAmount);

                pricingsRequest = pricingsRequest.Skip(req.Skip).Take(req.Take).OrderBy(z => z.StoneId).ToList();

                //Get Lastest 3 Discount Data
                res.PriceRequest = await GetLatestDiscountData(pricingsRequest);

                return res;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<PricingHistory>> GetPricingHistoryByStoneAsync(string stoneId) {
            try {
                List<PricingHistory> data = await _repository.GetAllAsync<PricingHistory>(z => z.StoneId == stoneId);
                return data.OrderByDescending(z => z.CreatedDate).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region Insert
        public async Task<bool> InsertTempPrices(IList<TempPricing> pricing) {
            try {
                var stoneIds = pricing.ToList().Select(a => a.StoneId).ToList();
                var existPricing = await _repository.GetAllAsync<TempPricing>(z => stoneIds.Contains(z.StoneId));
                if (existPricing.Count > 0)
                    await _repository.DeleteManyAsync(existPricing);

                pricing.ToList().ForEach(z => { z.Id = ""; });
                await _repository.AddManyAsync(pricing);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<string> InsertTempPrice(TempPricing pricing) {
            try {
                var existPricing = await _repository.GetOneAsync<TempPricing>(z => pricing.StoneId == z.StoneId);
                if (existPricing != null)
                    await _repository.DeleteOneAsync(existPricing);

                pricing.Id = "";
                await _repository.AddOneAsync(pricing);

                return pricing.Id;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> InsertPricingRequestFromInvItems(IList<InventoryItems> inv, string status) {
            try {
                var stoneIds = inv.ToList().Select(a => a.StoneId).ToList();
                var existPricing = await _repository.GetAllAsync<PricingRequest>(z => stoneIds.Contains(z.StoneId));
                if (existPricing.Count > 0)
                    await _repository.DeleteManyAsync(existPricing);

                List<PricingRequest> pricingReq = new List<PricingRequest>();
                inv.ToList().ForEach(z => {
                    pricingReq.Add(_mapping.MappingInvToPricingRequest(z, status));
                });

                await _repository.AddManyAsync(pricingReq);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> InsertPricingRequests(IList<PricingRequest> pricing) {
            try {
                var stoneIds = pricing.ToList().Select(a => a.StoneId).ToList();
                var existPricing = await _repository.GetAllAsync<PricingRequest>(z => stoneIds.Contains(z.StoneId));
                if (existPricing.Count > 0)
                    await _repository.DeleteManyAsync(existPricing);

                pricing.ToList().ForEach(z => { z.Id = ""; });
                await _repository.AddManyAsync(pricing);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<string> InsertPricingRequest(PricingRequest pricing) {
            try {
                var existPricing = await _repository.GetOneAsync<PricingRequest>(z => pricing.StoneId == z.StoneId);
                if (existPricing != null)
                    await _repository.DeleteOneAsync(existPricing);

                pricing.Id = "";
                await _repository.AddOneAsync(pricing);

                return pricing.Id;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<long> RemoveTempPrice(IList<TempPricing> pricing) {
            try {
                pricing.ToList().ForEach(async z => {
                    var inv = await _repository.GetOneAsync<InventoryItems>(a => a.StoneId == z.StoneId);

                    if (inv != null && z.TempPrice != null) {
                        inv.BasePrice = z.TempPrice;
                        inv.UpdatedBy = z.UpdatedBy;
                        inv.IsPricingRequest = false;

                        await _invRepository.UpdateInventoryAsync(inv);
                    }
                });

                return await _repository.DeleteManyAsync(pricing);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<long> RemovePricingRequest(IList<PricingRequest> pricing) {
            try {
                pricing.ToList().ForEach(async z => {
                    var inv = await _repository.GetOneAsync<InventoryItems>(a => a.StoneId == z.StoneId);

                    if (inv != null && z.TempPrice != null) {
                        inv.BasePrice = z.TempPrice;
                        inv.UpdatedBy = z.UpdatedBy;
                        inv.IsPricingRequest = false;

                        await _invRepository.UpdateInventoryAsync(inv);
                    }
                });

                return await _repository.DeleteManyAsync(pricing);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region AddOn Functions
        private async Task<List<TempPricing>> GetLatestDiscountData(List<TempPricing> tempPricing) {
            var stoneIds = tempPricing.Select(z => z.StoneId).ToList();
            var allData = await _repository.GetAllAsync<PricingHistory>(z => stoneIds.Contains(z.StoneId));

            tempPricing.ForEach(z => {
                List<PricingHistory> history = allData.Where(a => a.StoneId == z.StoneId).OrderByDescending(a => a.CreatedDate).Take(3).ToList();
                if (history.Count > 0) {
                    if (history[0] != null && history[0].Price != null)
                        z.DiscountOne = history[0].Price.Discount;
                    if (history.Count > 1 && history[1] != null && history[1].Price != null)
                        z.DiscountTwo = history[1].Price.Discount;
                    if (history.Count > 2 && history[2] != null && history[2].Price != null)
                        z.DiscountThree = history[2].Price.Discount;
                }
            });

            return tempPricing;
        }

        private async Task<List<PricingRequest>> GetLatestDiscountData(List<PricingRequest> pricing) {
            var stoneIds = pricing.Select(z => z.StoneId).ToList();
            var allData = await _repository.GetAllAsync<PricingHistory>(z => stoneIds.Contains(z.StoneId));

            pricing.ForEach(z => {
                List<PricingHistory> history = allData.Where(a => a.StoneId == z.StoneId).OrderByDescending(a => a.CreatedDate).Take(3).ToList();
                if (history.Count > 0) {
                    if (history[0] != null && history[0].Price != null)
                        z.DiscountOne = history[0].Price.Discount;
                    if (history.Count > 1 && history[1] != null && history[1].Price != null)
                        z.DiscountTwo = history[1].Price.Discount;
                    if (history.Count > 2 && history[2] != null && history[2].Price != null)
                        z.DiscountThree = history[2].Price.Discount;
                }
            });

            return pricing;
        }

        private List<TempPricing> GetTempPricingFromInv(List<InventoryItems> inventory, List<TempPricing> existingData) {
            List<TempPricing> tempPricings = new List<TempPricing>();

            inventory.ForEach(z => {
                var exist = existingData.FirstOrDefault(a => a.StoneId == z.StoneId);
                if (exist != null)
                    tempPricings.Add(exist);
                else {
                    TempPricing data = _mapping.MappingInvToTempPricing(z);
                    tempPricings.Add(data);
                }
            });

            return tempPricings;
        }

        private bool FilterData(InventoryItems target, IList<EmployeeCriteria> slots) {
            var flag = false;

            slots.ToList().ForEach(z => {
                if (!flag)
                    flag = SlotWiseFilter(target, z);
            });

            return flag;
        }

        private bool SlotWiseFilter(InventoryItems target, EmployeeCriteria filter) {
            return _filters.CompareArrayString(target.Shape, filter.Shape.ToList())
                && _filters.CompareArrayString(target.Lab, filter.Lab.ToList())
                && _filters.CompareArrayString(target.Color, filter.Color.ToList())
                && _filters.CompareArrayString(target.Clarity, filter.Clarity.ToList())
                && _filters.CompareArrayString(target.Cut, filter.Cut.ToList())
                && _filters.CompareArrayString(target.Polish, filter.Polish.ToList())
                && _filters.CompareArrayString(target.Symmetry, filter.Symmetry.ToList())
                && _filters.CompareArrayString(target.Fluorescence, filter.Fluorescence.ToList())
                //&& _filters.CompareArrayString(target.StoneOrg.OrgName, filter.Organizations.ToList())
                && _filters.CheckFromToDecimalValues(target.Weight, filter.MinWeight, filter.MaxWeight);
        }
        #endregion

    }
}