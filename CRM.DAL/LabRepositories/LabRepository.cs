using CRM.Business.Common;
using CRM.Business.Labs;
using CRM.DAL.BaseRepositories;
using CRM.DAL.InventoryRepositories;
using CRM.Entities.MongoEntities.GradingMaster;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Lab;
using CRM.Entities.MongoEntities.LabCollection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.DAL.LabRepositories {
    public class LabRepository : ILabRepository {
        private readonly IMongoRepository _repository;
        private readonly IInventoryRepository _invRepository;

        public LabRepository(IMongoRepository repository, IInventoryRepository invRepository) {
            _repository = repository;
            _invRepository = invRepository;
        }

        public async Task<Lab> GetLabAsync(string id) {
            try {
                return await _repository.GetOneAsync<Lab>(c => c.Id == id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<Lab>> GetLabsAsync() {
            try {
                return await _repository.GetAllAsync<Lab>(c => c.Id != null);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<LabResponse> GetLabsAsync(LabSearchCriteria OrganizationCriteria, int skip, int take) {
            try {
                var labResponse = new LabResponse();
                Expression<Func<Lab, bool>> filter = c => (string.IsNullOrEmpty(OrganizationCriteria.Name) || c.Name.ToLower().Contains(OrganizationCriteria.Name.ToLower()))
                && c.Id != null;

                labResponse.Labs = await _repository.GetPaginatedAsync<Lab>(filter, skip, take);
                labResponse.TotalCount = await _repository.CountAsync<Lab>(filter);

                return labResponse;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<string> InsertLabAsync(Lab lab) {
            try {
                await _repository.AddOneAsync(lab);

                return lab.Id;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateLabAsync(Lab lab) {
            try {
                var result = await _repository.UpdateOneAsync<Lab>(lab);

                return result;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<long> DeleteLabAsync(string id) {
            try {
                return await _repository.DeleteOneAsync<Lab>(c => c.Id == id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<CommonResponse> InsertLabIssue(LabIssue labIssue) {
            try {
                CommonResponse res = new CommonResponse();
                await _repository.AddOneAsync(labIssue);

                foreach (var items in labIssue.LabIssueItems) {
                    var inv = await _repository.GetOneAsync<InventoryItems>(a => a.StoneId == items.StoneId);

                    if (inv != null) {
                        inv.UpdatedBy = labIssue.Identity.Id;
                        inv.UpdatedAt = DateTime.UtcNow;
                        inv.LabSendDate = DateTime.UtcNow;
                        inv.Status = "Lab";
                        await _invRepository.UpdateInventoryAsync(inv);
                    }
                }
                res.IsSuccess = true;
                res.Message = labIssue.Id;
                return res;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<LabIssue>> GetLabsIssueAsync() {
            try {
                return await _repository.GetAllAsync<LabIssue>(c => c.Id != null);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateLabIssueAsync(LabIssue labIssue) {
            try {
                var result = await _repository.UpdateOneAsync<LabIssue>(labIssue);

                return result;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<GradingMaster>> GetGradingFromStoneIdsAsync(IList<string> ids) {
            try {
                var stoneIds = ids.ToList();
                var allData = await _repository.GetAllAsync<GradingMaster>(z => stoneIds.Contains(z.StoneId));
                return allData;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<InventoryItems>> GetInvItemsFromStoneIdsAsync(IList<string> ids) {
            try {
                var stoneIds = ids.ToList();
                var allData = await _repository.GetAllAsync<InventoryItems>(z => stoneIds.Contains(z.StoneId));
                return allData;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> InsertLabResult(IList<LabResult> labResult) {
            try {
                await _repository.AddManyAsync(labResult);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<bool> UpdateLabReceiveAsync(IList<LabReceiveItem> labReceiveItem) {
            try {
                var flag = false;
                if (labReceiveItem != null && labReceiveItem.Count > 0) {
                    foreach (var items in labReceiveItem) {
                        //Remove Record.
                        var remove = Builders<LabIssue>.Update.PullFilter(c => c.LabIssueItems, f => f.StoneId == items.StoneId);
                        var labIssue = await _repository.GetOneAsync<LabIssue>(c => c.Id == items.LabIssueID);
                        var labIssueItem = labIssue.LabIssueItems.Where(a => a.StoneId == items.StoneId && a.IsReceived == false).FirstOrDefault();
                        flag = await _repository.UpdateOneAsync<LabIssue>(labIssue, remove);

                        //After remove update that record.
                        if (labIssueItem != null && flag) {
                            labIssueItem.IsReceived = true;
                            labIssueItem.ReceiveDate = DateTime.UtcNow;
                            var update = Builders<LabIssue>.Update.Push(c => c.LabIssueItems, labIssueItem);
                            flag = await _repository.UpdateOneAsync<LabIssue>(labIssue, update);
                        }
                        if (flag == true) {
                            //Update Inventory Items after LabIssue Update.
                            labIssue.UpdatedBy = items.Identity.Id;
                            labIssue.UpdatedAt = DateTime.UtcNow;
                            var result = UpdateLabIssueAsync(labIssue);
                            var inv = await _repository.GetOneAsync<InventoryItems>(a => a.StoneId == items.StoneId);
                            if (inv != null) {
                                inv.UpdatedBy = items.Identity.Id;
                                inv.UpdatedAt = DateTime.UtcNow;
                                inv.LabReceiveDate = DateTime.UtcNow;
                                inv.Status = "Stock";
                                await _invRepository.UpdateInventoryAsync(inv);
                            }
                        }
                    }
                }
                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<CommonResponse> InsertLabRecheckIssueAsync(LabIssue labIssue) {
            try {
                CommonResponse res = new CommonResponse();
                LabIssue labRecheckIssue = new LabIssue();
                var flag = false;

                if (labIssue != null && labIssue.LabIssueItems.Count > 0) {
                    foreach (var items in labIssue.LabIssueItems) {

                        var getLabIssue = await _repository.GetOneAsync<LabIssue>(a => a.LabIssueItems.Any(z => z.StoneId == items.StoneId) && a.IsRecheck == false);
                        var getLabIssueNew = await _repository.GetOneAsync<LabIssue>(a => a.LabIssueItems.Any(z => z.StoneId == items.StoneId) && a.IsRecheck == false);

                        //Remove Record.
                        var remove = Builders<LabIssue>.Update.PullFilter(c => c.LabIssueItems, f => f.StoneId == items.StoneId);
                        var labIssueItem = getLabIssue.LabIssueItems.Where(a => a.StoneId == items.StoneId && a.ReceiveDate == null).FirstOrDefault();
                        flag = await _repository.UpdateOneAsync<LabIssue>(getLabIssue, remove);

                        //After remove update that record.
                        if (labIssueItem != null && flag) {
                            labIssueItem.IsReceived = true;
                            labIssueItem.ReceiveDate = DateTime.UtcNow;
                            var update = Builders<LabIssue>.Update.Push(c => c.LabIssueItems, labIssueItem);
                            flag = await _repository.UpdateOneAsync<LabIssue>(getLabIssue, update);
                        }

                        //Add New LabIssue with recheck reason.
                        labRecheckIssue.IsRecheck = true;
                        labRecheckIssue.RecheckReason = labIssue.RecheckReason;
                        labRecheckIssue.Identity.Id = labIssue.Identity.Id;
                        labRecheckIssue.Identity.Name = labIssue.Identity.Name;
                        labRecheckIssue.Identity.Type = labIssue.Identity.Type;
                        labRecheckIssue.LabIssueItems.Add(getLabIssueNew.LabIssueItems.FirstOrDefault(a => a.StoneId == items.StoneId));
                    }
                    await _repository.AddOneAsync(labRecheckIssue);//Add One Record In LabIssue For Recheck with Multiple Recheck Stones.
                }
                res.IsSuccess = true;
                res.Message = labIssue.Id;
                return res;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<bool> UpdateControllerNoFromStoneIds(IList<string> ids) {
            try {
                var flag = true;
                var stoneIds = ids.ToList();
                var labResults = await _repository.GetAllAsync<LabResult>(c => stoneIds.Contains(c.ClientRef));
                var invItems = await _repository.GetAllAsync<InventoryItems>(c => stoneIds.Contains(c.StoneId));

                invItems.ForEach(async z => {
                    var labResult = labResults.FirstOrDefault(a => a.ClientRef == z.StoneId);
                    if (labResult != null) {
                        z.RFID = labResult.ControlNo;
                        z.UpdatedAt = DateTime.UtcNow;
                        await _repository.UpdateOneAsync(z);
                    }
                });
                return flag;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<IList<string>> GetStoneIdsExistOrNotLabResultAsync(IList<string> ids) {
            try {
                IList<string> ExistIds = new List<string>();
                if (ids != null && ids.Count > 0) {
                    foreach (var items in ids) {
                        var result = await _repository.GetOneAsync<LabResult>(c => c.ClientRef == items);
                        if (result != null)
                            ExistIds.Add(result.ClientRef);
                    }
                }
                return ExistIds;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}
