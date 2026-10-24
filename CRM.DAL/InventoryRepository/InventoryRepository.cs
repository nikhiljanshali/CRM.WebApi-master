using CRM.Business.Inventory;
using CRM.DAL.BaseRepositories;
using CRM.DAL.GradingRepositories;
using CRM.DAL.PricingRepositories;
using CRM.DAL.CommonRepositories;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using CRM.Entities.MongoEntities.Pricing;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DAL.InventoryRepositories {
    public class InventoryRepository : IInventoryRepository {
        private readonly IMongoRepository _repository;
        private readonly IGradingRepository _gradingRepository;
        private readonly Mapping _mapping;
        private readonly Filters _filters;

        public InventoryRepository(IMongoRepository repository, IGradingRepository gradingRepository) {
            _repository = repository;
            _gradingRepository = gradingRepository;
            _mapping = new Mapping(_repository);
            _filters = new Filters(_repository);
        }

        #region Get
        public async Task<InventoryItems> GetInventoryAsync(string id) {
            try {
                return await _repository.GetOneAsync<InventoryItems>(c => c.Id == id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InventoryItems> GetInventoryByStoneIdAsync(string stoneId) {
            try {
                return await _repository.GetOneAsync<InventoryItems>(c => c.StoneId.ToLower() == stoneId.ToLower());
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<InventoryItems>> GetInventoriesAsync() {
            try {
                return await _repository.GetAllAsync<InventoryItems>(c => c.Id != null);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<InventoryItems>> GetInventoriesWithOutRFIDAsync() {
            try {
                return await _repository.GetAllAsync<InventoryItems>(c => c.Id != null && (c.RFID == null || c.RFID == ""));
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InventoryResponse> GetInventoriesAsync(InventorySearchCriteria InventoryCriteria, int skip, int take) {
            try {
                var invResponse = new InventoryResponse();

                var allInv = await _repository.GetAllAsync<InventoryItems>(c => (string.IsNullOrEmpty(InventoryCriteria.OrganizationId) || c.StoneOrg.OrgId == InventoryCriteria.OrganizationId) && c.Id != null);
                var filterInv = allInv.Where(c => _filters.FilterInvData(c, InventoryCriteria)).ToList();

                invResponse.TotalCount = filterInv.Count();
                invResponse.TotalWeight = filterInv.Sum(z => z.Weight);
                invResponse.TotalNetAmount = filterInv.Sum(z => z.BasePrice.NetAmount);
                invResponse.Inventories = filterInv.Skip(skip).Take(take).ToList();

                return invResponse;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InventoryItemMeasurement> GetMeasurementAsync(string inventoryId) {
            try {
                var measurement = await _repository.ProjectOneAsync<InventoryItems, InventoryItemMeasurement>(c => c.Id == inventoryId, c => c.Measurement);

                return measurement;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InventoryItemInclusion> GetInclusionAsync(string inventoryId) {
            try {
                var inclusion = await _repository.ProjectOneAsync<InventoryItems, InventoryItemInclusion>(c => c.Id == inventoryId, c => c.Inclusion);

                return inclusion;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<string>> GetStoneIdsExistOrNotAsync(IList<string> ids) {
            try {
                IList<string> ExistIds = new List<string>();
                if (ids != null && ids.Count > 0) {
                    foreach (var items in ids) {
                        var result = await _repository.GetOneAsync<InventoryItems>(c => c.StoneId == items);
                        if (result != null)
                            ExistIds.Add(result.StoneId);
                    }
                }
                return ExistIds;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region Insert
        public async Task<string> InsertInventoryAsync(InventoryItems inventory) {
            try {
                var IsExistStock = await _repository.GetOneAsync<InventoryItems>(c => c.StoneId == inventory.StoneId);
                if (IsExistStock != null)
                    return "Stock Id is already Exist";

                await _repository.AddOneAsync(inventory);
                return inventory.Id;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region Update
        public async Task<IList<string>> UploadInventoryFileAsync(IList<InventoryExcelItems> inventoryExcelItems) {
            try {
                IList<string> Ids = new List<string>();
                if (inventoryExcelItems != null && inventoryExcelItems.Count > 0) {

                    List<InventoryItems> invItems = new List<InventoryItems>();
                    List<PricingRequest> pricingReq = new List<PricingRequest>();

                    var stoneIds = inventoryExcelItems.Select(z => z.StoneId.ToLower()).ToList();
                    var allStone = await GetInventoriesAsync();
                    allStone = allStone.Where(c => stoneIds.Contains(c.StoneId, StringComparer.CurrentCultureIgnoreCase)).ToList();

                    foreach (var items in inventoryExcelItems) {
                        var result = allStone.FirstOrDefault(c => c.StoneId == items.StoneId);

                        if (result == null) {
                            InventoryItems invItem = _mapping.MappingInvExcelToInvItems(items, "Employee");

                            if (items.Discount == null) {
                                invItem.IsPricingRequest = true;
                                pricingReq.Add(_mapping.MappingInvToPricingRequest(invItem, "InvUpload"));
                            }

                            invItems.Add(invItem);
                            Ids.Add(invItem.StoneId);
                        }
                    }

                    //Add in Inventory Items
                    await _repository.AddManyAsync(invItems);

                    //Add in Grading Plan
                    await _gradingRepository.InsertGradingAsync(invItems, "InvUpload");

                    //Add in Pricing Request
                    await _repository.AddManyAsync(pricingReq);

                    //Add in Pricing Histories
                    await InsertPricingHistories(invItems);
                }

                return Ids;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<string>> UploadInclusionFileAsync(IList<InclusionExcelItems> inclusionExcelItems) {
            try {
                IList<string> Ids = new List<string>();

                var stoneIds = inclusionExcelItems.Select(z => z.StoneId.ToLower()).ToList();
                var allStone = await GetInventoriesAsync();
                allStone = allStone.Where(c => stoneIds.Contains(c.StoneId, StringComparer.CurrentCultureIgnoreCase)).ToList();

                foreach (var items in inclusionExcelItems) {
                    var inv = allStone.FirstOrDefault(c => c.StoneId.ToLower() == items.StoneId.ToLower());

                    if (inv != null) {
                        InventoryItems invItem = _mapping.MappingIncExcelToInvItems(items, inv);
                        invItem.UpdatedAt = DateTime.UtcNow;

                        var result = await _repository.UpdateOneAsync(invItem);

                        if (result)
                            Ids.Add(invItem.StoneId);
                    }
                }
                return Ids;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateInventoryAsync(InventoryItems inventory) {
            try {
                inventory.UpdatedAt = DateTime.UtcNow;

                var result = await _repository.UpdateOneAsync<InventoryItems>(inventory);
                await InsertPricingHistory(inventory);

                return result;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<string>> UpdateInventoryItemsAsync(IList<InventoryItems> inventoryItems) {
            try {
                IList<string> Ids = new List<string>();
                if (inventoryItems != null && inventoryItems.Count > 0) {
                    foreach (var items in inventoryItems) {
                        InventoryItems invItem = new InventoryItems();
                        invItem = await _repository.GetOneAsync<InventoryItems>(c => c.StoneId == items.StoneId);
                        Ids.Add(invItem.StoneId);
                        invItem.Shape = items.Shape;
                        invItem.Weight = items.Weight;
                        invItem.Color = items.Color;
                        invItem.Clarity = items.Clarity;
                        invItem.Cut = items.Cut;
                        invItem.Polish = items.Polish;
                        invItem.Symmetry = items.Symmetry;
                        invItem.Fluorescence = items.Fluorescence;
                        invItem.Comments = items.Comments;

                        invItem.Measurement.Depth = items.Measurement.Depth;
                        invItem.Measurement.Table = items.Measurement.Table;
                        invItem.Measurement.Length = items.Measurement.Length;
                        invItem.Measurement.Width = items.Measurement.Width;
                        invItem.Measurement.CrownHeight = items.Measurement.CrownHeight;
                        invItem.Measurement.CrownAngle = items.Measurement.CrownAngle;
                        invItem.Measurement.PavilionDepth = items.Measurement.PavilionDepth;
                        invItem.Measurement.PavilionAngle = items.Measurement.PavilionAngle;
                        invItem.Measurement.GirdlePer = items.Measurement.GirdlePer;
                        invItem.Measurement.MaxGirdle = items.Measurement.MaxGirdle;
                        invItem.Measurement.MinGirdle = items.Measurement.MinGirdle;
                        invItem.Inclusion.Culet = items.Inclusion.Culet;
                        invItem.Inclusion.KtoS = items.Inclusion.KtoS;
                        invItem.Inclusion.GirdleCondition = items.Inclusion.GirdleCondition;

                        invItem.BasePrice.Rap = items.BasePrice.Rap;
                        invItem.BasePrice.Discount = items.BasePrice.Discount;
                        invItem.BasePrice.NetAmount = items.BasePrice.NetAmount;
                        invItem.BasePrice.PerCarat = items.BasePrice.PerCarat;

                        invItem.Price.Rap = items.BasePrice.Rap;
                        invItem.Price.Discount = items.BasePrice.Discount;
                        invItem.Price.NetAmount = items.BasePrice.NetAmount;
                        invItem.Price.PerCarat = items.BasePrice.PerCarat;

                        invItem.CertificateNo = items.CertificateNo;

                        invItem.UpdatedBy = items.Identity.Id;
                        invItem.UpdatedAt = DateTime.Now.ToUniversalTime();

                        await UpdateInventoryAsync(invItem);
                    }
                }
                return Ids;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateInventoryMeasurementAsync(string inventoryId, InventoryItemMeasurement measurement) {
            try {
                bool flag = false;

                var inventory = await _repository.GetOneAsync<InventoryItems>(c => c.Id == inventoryId);
                if (inventory != null && !string.IsNullOrEmpty(inventory.Id)) {
                    var update = Builders<InventoryItems>.Update.Set(c => c.Measurement, measurement);
                    flag = await _repository.UpdateOneAsync<InventoryItems>(inventory, update);
                }

                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateInventoryInclusionAsync(string inventoryId, InventoryItemInclusion inclusion) {
            try {
                bool flag = false;

                var inventory = await _repository.GetOneAsync<InventoryItems>(c => c.Id == inventoryId);
                if (inventory != null && !string.IsNullOrEmpty(inventory.Id)) {
                    var update = Builders<InventoryItems>.Update.Set(c => c.Inclusion, inclusion);
                    flag = await _repository.UpdateOneAsync<InventoryItems>(inventory, update);
                }

                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<long> UpdateInventoryHoldAsync(IList<InventoryItems> inventory) {
            try {
                long flag = 0;

                if (inventory != null && inventory.Count > 0) {
                    var filter = Builders<InventoryItems>.Filter.In(c => c.StoneId, inventory.Select(v => v.StoneId));
                    var update = Builders<InventoryItems>.Update.Set(c => c.IsHold, inventory[0].IsHold).Set(c => c.IsRapnetHold, inventory[0].IsRapnetHold);

                    flag = await _repository.UpdateManyAsync<InventoryItems>(filter, update);

                }
                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateInventoryListAsync(IList<InventoryItems> inventoryItems) {
            try {
                bool flag = false;
                if (inventoryItems != null && inventoryItems.Count > 0) {
                    for (int i = 0; i < inventoryItems.Count; i++) {
                        var inventory = inventoryItems[i];
                        flag = await _repository.UpdateOneAsync(inventory);
                        if (flag == false)
                            return flag;
                    }
                }
                return flag;
            }
            catch (Exception ex) {

                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<long> DeleteInventoryAsync(string id) {
            try {
                return await _repository.DeleteOneAsync<InventoryItems>(c => c.Id == id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> DeleteInventoryMeasurementAsync(string inventoryId) {
            try {
                bool flag = false;

                var inventory = await _repository.GetOneAsync<InventoryItems>(c => c.Id == inventoryId);
                if (inventory != null && !string.IsNullOrEmpty(inventory.Id)) {
                    if (inventory.Measurement != null) {
                        inventory.Measurement = new InventoryItemMeasurement();
                        flag = await _repository.UpdateOneAsync<InventoryItems>(inventory);
                    }
                }
                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> DeleteInventoryInclusionAsync(string inventoryId) {
            try {
                bool flag = false;

                var inventory = await _repository.GetOneAsync<InventoryItems>(c => c.Id == inventoryId);
                if (inventory != null && !string.IsNullOrEmpty(inventory.Id)) {
                    if (inventory.Inclusion != null) {
                        inventory.Inclusion = new InventoryItemInclusion();
                        flag = await _repository.UpdateOneAsync<InventoryItems>(inventory);
                    }
                }
                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

        #region AddOn Functions
        private async Task<bool> InsertPricingHistories(IList<InventoryItems> inventories) {
            try {
                List<PricingHistory> pricings = new List<PricingHistory>();

                inventories.ToList().ForEach(z => {
                    PricingHistory pricing = _mapping.MappingInvToPricingHistory(z);

                    pricings.Add(pricing);
                });

                await _repository.AddManyAsync(pricings);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private async Task<bool> InsertPricingHistory(InventoryItems inventory) {
            try {
                PricingHistory pricing = _mapping.MappingInvToPricingHistory(inventory);

                await _repository.AddOneAsync(pricing);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        #endregion

    }
}