using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.GradingMaster;
using CRM.Entities.MongoEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DAL.GradingRepositories {
    public class GradingRepository : IGradingRepository {
        private readonly IMongoRepository _repository;

        public GradingRepository(IMongoRepository repository) {
            _repository = repository;
        }

        public async Task<IList<GradingMaster>> GetGradingAsync(string StoneId) {
            return await _repository.GetAllAsync<GradingMaster>(c => c.StoneId == StoneId);
        }

        public async Task<IList<string>> InsertGradingAsync(IList<InventoryItems> inventoryItems,string RepVer) {
            try {
                IList<string> Ids = new List<string>();
                List<GradingMaster> GradingMasterItems = new List<GradingMaster>();
                if (inventoryItems != null && inventoryItems.Count > 0) {

                    foreach (var items in inventoryItems) {
                        GradingMaster GradingMasterItem = new GradingMaster();
                        GradingMasterItem.StoneId = items.StoneId;
                        GradingMasterItem.Kapan = items.Kapan;
                        GradingMasterItem.Article = items.Article;
                        GradingMasterItem.Grade = items.Grade;
                        GradingMasterItem.Shape = items.Shape;
                        GradingMasterItem.Weight = items.Weight;
                        GradingMasterItem.Color = items.Color;
                        GradingMasterItem.Clarity = items.Clarity;
                        GradingMasterItem.Cut = items.Cut;
                        GradingMasterItem.Polish = items.Polish;
                        GradingMasterItem.Symmetry = items.Symmetry;
                        GradingMasterItem.Fluorescence = items.Fluorescence;
                        GradingMasterItem.StrLn = items.StrLn;
                        GradingMasterItem.LrHalf = items.LrHalf;
                        GradingMasterItem.Comments = items.Comments;
                        GradingMasterItem.Lab = items.Lab;

                        GradingMasterItem.BasePrice.Rap = items.BasePrice.Rap;
                        GradingMasterItem.BasePrice.Discount = items.BasePrice.Discount;
                        GradingMasterItem.BasePrice.NetAmount = items.BasePrice.NetAmount;
                        GradingMasterItem.BasePrice.PerCarat = items.BasePrice.PerCarat;

                        GradingMasterItem.Measurement.Depth = items.Measurement.Depth;
                        GradingMasterItem.Measurement.Table = items.Measurement.Table;
                        GradingMasterItem.Measurement.Length = items.Measurement.Length;
                        GradingMasterItem.Measurement.Width = items.Measurement.Width;
                        GradingMasterItem.Measurement.CrownHeight = items.Measurement.CrownHeight;
                        GradingMasterItem.Measurement.CrownAngle = items.Measurement.CrownAngle;
                        GradingMasterItem.Measurement.PavilionAngle = items.Measurement.PavilionAngle;
                        GradingMasterItem.Measurement.PavilionDepth = items.Measurement.PavilionDepth;
                        GradingMasterItem.Measurement.GirdlePer = items.Measurement.GirdlePer;
                        GradingMasterItem.Measurement.MinGirdle = items.Measurement.MinGirdle;
                        GradingMasterItem.Measurement.MaxGirdle = items.Measurement.MaxGirdle;
                        GradingMasterItem.Measurement.Ratio = items.Measurement.Ratio;
                        GradingMasterItem.Inclusion.Brown = items.Inclusion.Brown;
                        GradingMasterItem.Inclusion.Green = items.Inclusion.Green;
                        GradingMasterItem.Inclusion.Milky = items.Inclusion.Milky;
                        GradingMasterItem.Inclusion.Shade = items.Inclusion.Shade;
                        GradingMasterItem.Inclusion.SideBlack = items.Inclusion.SideBlack;
                        GradingMasterItem.Inclusion.CenterSideBlack = items.Inclusion.CenterSideBlack;
                        GradingMasterItem.Inclusion.CenterBlack = items.Inclusion.CenterBlack;
                        GradingMasterItem.Inclusion.SideWhite = items.Inclusion.SideWhite;
                        GradingMasterItem.Inclusion.CenterSideWhite = items.Inclusion.CenterSideWhite;
                        GradingMasterItem.Inclusion.OpenCrown = items.Inclusion.OpenCrown;
                        GradingMasterItem.Inclusion.OpenTable = items.Inclusion.OpenTable;
                        GradingMasterItem.Inclusion.OpenPavilion = items.Inclusion.OpenPavilion;
                        GradingMasterItem.Inclusion.OpenGirdle = items.Inclusion.OpenGirdle;
                        GradingMasterItem.Inclusion.GirdleCondition = items.Inclusion.GirdleCondition;
                        GradingMasterItem.Inclusion.EFOC = items.Inclusion.EFOC;
                        GradingMasterItem.Inclusion.EFOP = items.Inclusion.EFOP;
                        GradingMasterItem.Inclusion.Culet = items.Inclusion.Culet;
                        GradingMasterItem.Inclusion.HNA = items.Inclusion.HNA;
                        GradingMasterItem.Inclusion.EyeClean = items.Inclusion.EyeClean;
                        GradingMasterItem.Inclusion.KtoS = items.Inclusion.KtoS;
                        GradingMasterItem.Inclusion.NaturalOnTable = items.Inclusion.NaturalOnTable;
                        GradingMasterItem.Inclusion.NaturalOnGirdle = items.Inclusion.NaturalOnGirdle;
                        GradingMasterItem.Inclusion.NaturalOnCrown = items.Inclusion.NaturalOnCrown;
                        GradingMasterItem.Inclusion.NaturalOnPavillion = items.Inclusion.NaturalOnPavillion;
                        GradingMasterItem.Inclusion.FLColor = items.Inclusion.FLColor;
                        GradingMasterItem.Inclusion.Luster = items.Inclusion.Luster;
                        GradingMasterItem.Inclusion.Bowtie = items.Inclusion.Bowtie;

                        GradingMasterItem.StoneOrg.OrgId = items.StoneOrg.OrgId;
                        GradingMasterItem.StoneOrg.OrgName = items.StoneOrg.OrgName;
                        GradingMasterItem.StoneOrg.DeptId = items.StoneOrg.DeptId;
                        GradingMasterItem.StoneOrg.DeptName = items.StoneOrg.DeptName;
                        GradingMasterItem.StoneOrg.BranchName = items.StoneOrg.BranchName;
                        GradingMasterItem.StoneOrg.Country = items.StoneOrg.Country;
                        GradingMasterItem.StoneOrg.City = items.StoneOrg.City;

                        GradingMasterItem.Identity.Id = items.Identity.Id;
                        GradingMasterItem.Identity.Name = items.Identity.Name;
                        GradingMasterItem.Identity.Type = "Employee";

                        GradingMasterItem.CreatedBy = items.Identity.Id;
                        GradingMasterItem.RapVer = RepVer;

                        GradingMasterItems.Add(GradingMasterItem);
                        Ids.Add(GradingMasterItem.StoneId);
                    }
                    await _repository.AddManyAsync(GradingMasterItems);
                }
                return Ids;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InventoryItems> UpdateGradingAsync(GradingMaster GradingMaster) {
            try {
                var result = await _repository.UpdateOneAsync<GradingMaster>(GradingMaster);
                InventoryItems invItem = new InventoryItems();

                if (result == true) {
                    invItem = await _repository.GetOneAsync<InventoryItems>(c => c.StoneId == GradingMaster.StoneId);
                    invItem.StoneId = GradingMaster.StoneId;
                    invItem.Article = GradingMaster.Article;
                    invItem.Shape = GradingMaster.Shape;
                    invItem.Weight = GradingMaster.Weight;
                    invItem.Color = GradingMaster.Color;
                    invItem.Clarity = GradingMaster.Clarity;
                    invItem.Cut = GradingMaster.Cut;
                    invItem.Polish = GradingMaster.Polish;
                    invItem.Symmetry = GradingMaster.Symmetry;
                    invItem.Fluorescence = GradingMaster.Fluorescence;
                    invItem.BasePrice.Rap = GradingMaster.BasePrice.Rap;
                    invItem.BasePrice.Discount = GradingMaster.BasePrice.Discount;
                    invItem.BasePrice.NetAmount = GradingMaster.BasePrice.NetAmount;
                    invItem.BasePrice.PerCarat = GradingMaster.BasePrice.PerCarat;
                    invItem.Comments = GradingMaster.Comments;
                    invItem.Kapan = GradingMaster.Kapan;

                    invItem.Measurement.Depth = GradingMaster.Measurement.Depth;
                    invItem.Measurement.Table = GradingMaster.Measurement.Table;
                    invItem.Measurement.Length = GradingMaster.Measurement.Length;
                    invItem.Measurement.Width = GradingMaster.Measurement.Width;
                    invItem.Measurement.CrownHeight = GradingMaster.Measurement.CrownHeight;
                    invItem.Measurement.CrownAngle = GradingMaster.Measurement.CrownAngle;
                    invItem.Measurement.PavilionAngle = GradingMaster.Measurement.PavilionAngle;
                    invItem.Measurement.PavilionDepth = GradingMaster.Measurement.PavilionDepth;
                    invItem.Measurement.GirdlePer = GradingMaster.Measurement.GirdlePer;
                    invItem.Measurement.MinGirdle = GradingMaster.Measurement.MinGirdle;
                    invItem.Measurement.MaxGirdle = GradingMaster.Measurement.MaxGirdle;
                    invItem.Measurement.Ratio = GradingMaster.Measurement.Ratio;

                    invItem.Inclusion.Brown = GradingMaster.Inclusion.Brown;
                    invItem.Inclusion.Green = GradingMaster.Inclusion.Green;
                    invItem.Inclusion.Milky = GradingMaster.Inclusion.Milky;
                    invItem.Inclusion.Shade = GradingMaster.Inclusion.Shade;
                    invItem.Inclusion.SideBlack = GradingMaster.Inclusion.SideBlack;
                    invItem.Inclusion.CenterSideBlack = GradingMaster.Inclusion.CenterSideBlack;
                    invItem.Inclusion.CenterBlack = GradingMaster.Inclusion.CenterBlack;
                    invItem.Inclusion.SideWhite = GradingMaster.Inclusion.SideWhite;
                    invItem.Inclusion.CenterSideWhite = GradingMaster.Inclusion.CenterSideWhite;
                    invItem.Inclusion.OpenCrown = GradingMaster.Inclusion.OpenCrown;
                    invItem.Inclusion.OpenTable = GradingMaster.Inclusion.OpenTable;
                    invItem.Inclusion.OpenPavilion = GradingMaster.Inclusion.OpenPavilion;
                    invItem.Inclusion.OpenGirdle = GradingMaster.Inclusion.OpenGirdle;
                    invItem.Inclusion.GirdleCondition = GradingMaster.Inclusion.GirdleCondition;
                    invItem.Inclusion.EFOC = GradingMaster.Inclusion.EFOC;
                    invItem.Inclusion.EFOP = GradingMaster.Inclusion.EFOP;
                    invItem.Inclusion.Culet = GradingMaster.Inclusion.Culet;
                    invItem.Inclusion.HNA = GradingMaster.Inclusion.HNA;
                    invItem.Inclusion.EyeClean = GradingMaster.Inclusion.EyeClean;
                    invItem.Inclusion.KtoS = GradingMaster.Inclusion.KtoS;
                    invItem.Inclusion.NaturalOnTable = GradingMaster.Inclusion.NaturalOnTable;
                    invItem.Inclusion.NaturalOnGirdle = GradingMaster.Inclusion.NaturalOnGirdle;
                    invItem.Inclusion.NaturalOnCrown = GradingMaster.Inclusion.NaturalOnCrown;
                    invItem.Inclusion.NaturalOnPavillion = GradingMaster.Inclusion.NaturalOnPavillion;
                    invItem.Inclusion.FLColor = GradingMaster.Inclusion.FLColor;
                    invItem.Inclusion.Luster = GradingMaster.Inclusion.Luster;
                    invItem.Inclusion.Bowtie = GradingMaster.Inclusion.Bowtie;

                    invItem.StoneOrg.OrgId = GradingMaster.StoneOrg.OrgId;
                    invItem.StoneOrg.OrgName = GradingMaster.StoneOrg.OrgName;
                    invItem.StoneOrg.DeptId = GradingMaster.StoneOrg.DeptId;
                    invItem.StoneOrg.DeptName = GradingMaster.StoneOrg.DeptName;
                    invItem.StoneOrg.BranchName = GradingMaster.StoneOrg.BranchName;
                    invItem.StoneOrg.Country = GradingMaster.StoneOrg.Country;
                    invItem.StoneOrg.City = GradingMaster.StoneOrg.City;

                    invItem.UpdatedBy = GradingMaster.Identity.Id;
                    invItem.UpdatedAt = DateTime.Now.ToUniversalTime();
                }
                return invItem;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}
