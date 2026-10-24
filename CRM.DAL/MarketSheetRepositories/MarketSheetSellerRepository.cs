using CRM.Business.Inventory;
using CRM.Business.MarketSheets;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DAL.MarketSheetRepositories {
    public class MarketSheetSellerRepository : IMarketSheetSellerRepository {

        private readonly IMongoRepository _repository;

        public MarketSheetSellerRepository(IMongoRepository repository) {
            _repository = repository;
        }
        public async Task<MarketSheetSellerResponse> GetMarketSheetsAsync(MarketSheetSellerSearchCriteria MarketSheetSellerCriteria, int skip, int take) {
            try {
                var marketResponse = new MarketSheetSellerResponse();

                IList<InventoryItems> allInv = await _repository.GetAllAsync<InventoryItems>(c => (string.IsNullOrEmpty(MarketSheetSellerCriteria.OrganizationId) || c.StoneOrg.OrgId == MarketSheetSellerCriteria.OrganizationId)
                && (MarketSheetSellerCriteria.Location.Count == 0 || MarketSheetSellerCriteria.Location.Contains(c.Location))
                && ((MarketSheetSellerCriteria.IsHold == null) || (MarketSheetSellerCriteria.IsHold == c.IsHold))
                && ((MarketSheetSellerCriteria.IsRapnetHold == null) || (MarketSheetSellerCriteria.IsRapnetHold == c.IsRapnetHold))
                && c.Price.Rap != null
                && c.Price.NetAmount != null
                && c.Price.Discount != null
                && c.Price.PerCarat != null
                && c.MarketSheetDate != null
                && c.Id != null);

                IList<InventoryItems> isHoldStones = allInv.Where(c => c.IsHold == true).ToList();
                IList<InventoryItems> isRapNetHoldStones = allInv.Where(c => c.IsRapnetHold == true).ToList();
                IList<InventoryItems> filterInv = allInv.Where(c => FilterData(c, MarketSheetSellerCriteria)).ToList();
                IList<InventoryItems> isHKStones = filterInv.Where(c => c.Location != null && c.Location.ToLower() == "hongkong").ToList();
                IList<InventoryItems> isIndiaStones = filterInv.Where(c => c.Location != null && c.Location.ToLower() == "india").ToList();
                IList<InventoryItems> isBelgiumStones = filterInv.Where(c => c.Location != null && c.Location.ToLower() == "belgium").ToList();

                decimal? rapWeight = 0;
                marketResponse.TotalCount = filterInv.Count();
                marketResponse.TotalHolds = isHoldStones.Count();
                marketResponse.TotalRapNetHolds = isRapNetHoldStones.Count();
                marketResponse.TotalLeads = 0;
                marketResponse.TotalMemos = 0;
                marketResponse.TotalAvailable = marketResponse.TotalCount - marketResponse.TotalHolds - marketResponse.TotalLeads - marketResponse.TotalMemos;
                marketResponse.TotalHK = isHKStones.Count();
                marketResponse.TotalIndia = isIndiaStones.Count();
                marketResponse.TotalBelgium = isBelgiumStones.Count();

                marketResponse.TotalWeight = filterInv.Sum(z => z.Weight);
                marketResponse.TotalNetAmount = filterInv.Sum(z => z.Price.NetAmount);
                marketResponse.MarketSheets = filterInv.Skip(skip).Take(take).ToList();
                rapWeight = filterInv.Sum(z => z.Weight * z.Price.Rap);

                if (marketResponse.TotalNetAmount > 0 && marketResponse.TotalWeight > 0) {
                    var dCaret = marketResponse.TotalNetAmount / marketResponse.TotalWeight;
                    marketResponse.TotalAvgDPC = dCaret;
                }

                if (rapWeight > 0) {
                    var avgDisc = ((marketResponse.TotalNetAmount / rapWeight) * 100) - 100;
                    marketResponse.TotalDiscount = avgDisc;
                }

                return marketResponse;

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private bool FilterData(InventoryItems data, MarketSheetSellerSearchCriteria filter) {
            IList<WeightRange> range = new List<WeightRange>();
            if (filter.Carat != null) {
                if (filter.Carat.Contains(",")) {
                    var obj = filter.Carat.Split(",");
                    if (obj.Length > 0) {
                        foreach (var item in obj) {
                            WeightRange val = new WeightRange();
                            if (item.Contains("-")) {
                                var arr = item.Split("-");
                                val.MinWeight = Convert.ToDecimal(arr[0]);
                                val.MaxWeight = Convert.ToDecimal(arr[1]);
                                range.Add(val);
                            }
                        }
                    }
                }
                else {
                    if (filter.Carat.Contains("-")) {
                        WeightRange val = new WeightRange();
                        var arr = filter.Carat.Split("-");
                        val.MinWeight = Convert.ToDecimal(arr[0]);
                        val.MaxWeight = Convert.ToDecimal(arr[1]);
                        range.Add(val);
                    }
                }
            }

            return (filter.Carat == null || (range.Any(c => c.MinWeight <= data.Weight && c.MaxWeight >= data.Weight)))
                && (filter.FromDate == null || filter.ToDate == null || ((filter.FromDate != null && filter.ToDate != null) && filter.FromDate?.Date <= data.CreatedDate.Date && filter.ToDate?.Date >= data.CreatedDate.Date))
                && CheckCommonString(data.StoneOrg.BranchName, filter.Branch)
                && CheckCommonString(data.StoneOrg.DeptId, filter.DeptId)
                && CheckCommonString(data.Identity.Id, filter.EmpId)
                && CheckCommonString(data.StoneId, filter.StockId)
                && CheckCommonString(data.CertificateNo, filter.CertificateNo)
                && CheckCommonBoolen(data.IsHold, filter.IsHold)
                && CheckCommonBoolen(data.IsRapnetHold, filter.IsRapnetHold)
                && CheckFromToDecimalValues(data.Measurement.Ratio, filter.FromRatio, filter.ToRatio)
                && CheckFromToDecimalValues(data.Measurement.Depth, filter.FromDepth, filter.ToDepth)
                && CheckFromToDecimalValues(data.Measurement.Table, filter.FromTable, filter.ToTable)
                && CheckFromToDecimalValues(data.Measurement.Length, filter.FromLength, filter.ToLength)
                && CheckFromToDecimalValues(data.Measurement.Width, filter.FromWidth, filter.ToWidth)
                && CheckFromToDecimalValues(data.Measurement.Height, filter.FromHeight, filter.ToHeight)
                && CheckFromToDecimalValues(data.Measurement.CrownHeight, filter.FromCrownHeight, filter.ToCrownHeight)
                && CheckFromToDecimalValues(data.Measurement.CrownAngle, filter.FromCrownAngle, filter.ToCrownAngle)
                && CheckFromToDecimalValues(data.Measurement.PavilionDepth, filter.FromPavilionDepth, filter.ToPavilionDepth)
                && CheckFromToDecimalValues(data.Measurement.PavilionAngle, filter.FromPavilionAngle, filter.ToPavilionAngle)
                && CheckFromToDecimalValues(data.Measurement.GirdlePer, filter.FromGirdlePer, filter.ToGirdlePer)
                && CheckFromToDecimalValues(data.BasePrice.PerCarat, filter.FromPerCarat, filter.ToPerCarat)
                && CheckFromToDecimalValues(data.BasePrice.NetAmount, filter.FromNetAmt, filter.ToNetAmt)
                && CompareArrayString(data.Status, filter.Status)
                && CompareArrayString(data.Shape, filter.Shape)
                && CompareArrayString(data.Color, filter.Color)
                && CompareArrayString(data.Clarity, filter.Clarity)
                && CompareArrayString(data.Fluorescence, filter.Flour)
                && CompareArrayString(data.Inclusion.Shade, filter.Shade)
                && CompareArrayString(data.Lab, filter.Lab)
                && CompareArrayString(data.Cut, filter.Cut)
                && CompareArrayString(data.Polish, filter.Polish)
                && CompareArrayString(data.Symmetry, filter.Symm)
                && CompareArrayString(data.Inclusion.KtoS, filter.KToS)
                && CompareArrayString(data.Inclusion.Culet, filter.Culet)
                && CompareArrayString(data.Inclusion.SideBlack, filter.SideBlack)
                && CompareArrayString(data.Inclusion.CenterBlack, filter.CenterBlack)
                && CompareArrayString(data.Inclusion.SideWhite, filter.SideWhite)
                && CompareArrayString(data.Inclusion.CenterWhite, filter.CenterWhite)
                && CompareArrayString(data.Inclusion.Brown, filter.Brown)
                && CompareArrayString(data.Inclusion.Green, filter.Green)
                && CompareArrayString(data.Inclusion.Milky, filter.Milky)
                && CompareArrayString(data.Inclusion.OpenCrown, filter.OpenCrown)
                && CompareArrayString(data.Inclusion.OpenTable, filter.OpenTable)
                && CompareArrayString(data.Inclusion.OpenPavilion, filter.OpenPavilion)
                && CompareArrayString(data.Inclusion.OpenGirdle, filter.OpenGirdle)
                && CompareArrayString(data.Inclusion.NaturalOnGirdle, filter.NaturalOnGirdle)
                && CompareArrayString(data.Inclusion.NaturalOnCrown, filter.NaturalOnCrown)
                && CompareArrayString(data.Inclusion.NaturalOnPavillion, filter.NaturalOnPavillion)
                && CompareArrayString(data.Inclusion.NaturalOnTable, filter.NaturalOnTable)
                && CompareArrayString(data.Inclusion.HNA, filter.HNA)
                && CompareArrayString(data.Inclusion.EyeClean, filter.EyeClean)
                && CompareArrayString(data.Inclusion.EFOC, filter.EFOC)
                && CompareArrayString(data.Inclusion.EFOP, filter.EFOP)
                && CompareArrayString(data.Inclusion.EFOG, filter.EFOG)
                && CompareArrayString(data.Inclusion.EFOT, filter.EFOT)
                && CompareArrayString(data.Inclusion.GirdleCondition, filter.GirdleCondition)
                && CompareArrayString(data.Inclusion.Luster, filter.Luster)
                && CompareArrayString(data.Inclusion.Bowtie, filter.Bowtie);
        }

        private bool CompareArrayString(string target, List<string> filter) {
            bool result = true;

            if (filter != null && filter.Count > 0 && target != null && target.Length > 0)
                return filter.Contains(target, StringComparer.CurrentCultureIgnoreCase);

            return result;
        }

        private bool CheckFromToDecimalValues(decimal? target, decimal? filterFrom, decimal? filterTo) {
            bool result = true;

            if ((filterFrom != null && filterFrom > 0) && (filterTo != null && filterTo > 0) && target != null && target > 0)
                return (filterFrom <= target && filterTo >= target);

            return result;
        }

        private bool CheckCommonString(string target, string filter) {
            bool result = true;

            if (filter != null && filter.Length > 0 && target != null && target.Length > 0)
                return (target == filter);

            return result;
        }

        private bool CheckCommonBoolen(bool? target, bool? filter) {
            bool result = true;

            if (filter != null && target != null)
                return (target == filter);

            return result;
        }
    }
}
