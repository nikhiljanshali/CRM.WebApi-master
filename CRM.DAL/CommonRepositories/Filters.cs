using CRM.Business.Inventory;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.DAL.CommonRepositories {
    public class Filters {
        private readonly IMongoRepository _repository;
        public Filters(IMongoRepository repository) {
            _repository = repository;
        }

        public bool FilterInvData(InventoryItems target, InventorySearchCriteria filter) {
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

            List<string> stoneIds = new List<string>();
            if (filter.StockId != null) {
                if (filter.StockId.Contains(","))
                    stoneIds = filter.StockId.Split(",").ToList();
                else if (filter.StockId.Contains(" "))
                    stoneIds = filter.StockId.Split(" ").ToList();
                else
                    stoneIds.Add(filter.StockId);
            }

            return (filter.Carat == null || (range.Any(c => c.MinWeight <= target.Weight && c.MaxWeight >= target.Weight)))
                && (filter.FromDate == null || filter.ToDate == null || ((filter.FromDate != null && filter.ToDate != null) && filter.FromDate?.Date <= target.CreatedDate.Date && filter.ToDate?.Date >= target.CreatedDate.Date))
                && CheckCommonString(target.StoneOrg.BranchName, filter.Branch)
                && CheckCommonString(target.StoneOrg.DeptId, filter.DeptId)
                && CheckCommonString(target.Identity.Id, filter.EmpId)
                && CheckCommonString(target.CertificateNo, filter.CertificateNo)
                && CheckCommonString(target.Kapan, filter.Kapan)
                && CheckCommonBoolen(target.IsHold, filter.IsHold)
                && CheckCommonBoolen(target.IsRapnetHold, filter.IsRapnetHold)
                && CheckFromToNegativeDecimalValues(target.BasePrice.Discount, filter.FromDiscount, filter.ToDiscount)
                && CheckFromToDecimalValues(target.Measurement.Ratio, filter.FromRatio, filter.ToRatio)
                && CheckFromToDecimalValues(target.Measurement.Depth, filter.FromDepth, filter.ToDepth)
                && CheckFromToDecimalValues(target.Measurement.Table, filter.FromTable, filter.ToTable)
                && CheckFromToDecimalValues(target.Measurement.Length, filter.FromLength, filter.ToLength)
                && CheckFromToDecimalValues(target.Measurement.Width, filter.FromWidth, filter.ToWidth)
                && CheckFromToDecimalValues(target.Measurement.Height, filter.FromHeight, filter.ToHeight)
                && CheckFromToDecimalValues(target.Measurement.CrownHeight, filter.FromCrownHeight, filter.ToCrownHeight)
                && CheckFromToDecimalValues(target.Measurement.CrownAngle, filter.FromCrownAngle, filter.ToCrownAngle)
                && CheckFromToDecimalValues(target.Measurement.PavilionDepth, filter.FromPavilionDepth, filter.ToPavilionDepth)
                && CheckFromToDecimalValues(target.Measurement.PavilionAngle, filter.FromPavilionAngle, filter.ToPavilionAngle)
                && CheckFromToDecimalValues(target.Measurement.GirdlePer, filter.FromGirdlePer, filter.ToGirdlePer)
                && CheckFromToDecimalValues(target.BasePrice.PerCarat, filter.FromPerCarat, filter.ToPerCarat)
                && CheckFromToDecimalValues(target.BasePrice.NetAmount, filter.FromNetAmt, filter.ToNetAmt)
                && CompareArrayString(target.StoneId, stoneIds)
                && CompareArrayString(target.Location, filter.Location)
                && CompareArrayString(target.Status, filter.Status)
                && CompareArrayString(target.Shape, filter.Shape)
                && CompareArrayString(target.Color, filter.Color)
                && CompareArrayString(target.Clarity, filter.Clarity)
                && CompareArrayString(target.Fluorescence, filter.Flour)
                && CompareArrayString(target.Inclusion.Shade, filter.Shade)
                && CompareArrayString(target.Lab, filter.Lab)
                && CompareArrayString(target.Cut, filter.Cut)
                && CompareArrayString(target.Polish, filter.Polish)
                && CompareArrayString(target.Symmetry, filter.Symm)
                && CompareArrayString(target.Inclusion.KtoS, filter.KToS)
                && CompareArrayString(target.Inclusion.Culet, filter.Culet)
                && CompareArrayString(target.Inclusion.SideBlack, filter.SideBlack)
                && CompareArrayString(target.Inclusion.CenterBlack, filter.CenterBlack)
                && CompareArrayString(target.Inclusion.SideWhite, filter.SideWhite)
                && CompareArrayString(target.Inclusion.CenterWhite, filter.CenterWhite)
                && CompareArrayString(target.Inclusion.Brown, filter.Brown)
                && CompareArrayString(target.Inclusion.Green, filter.Green)
                && CompareArrayString(target.Inclusion.Milky, filter.Milky)
                && CompareArrayString(target.Inclusion.OpenCrown, filter.OpenCrown)
                && CompareArrayString(target.Inclusion.OpenTable, filter.OpenTable)
                && CompareArrayString(target.Inclusion.OpenPavilion, filter.OpenPavilion)
                && CompareArrayString(target.Inclusion.OpenGirdle, filter.OpenGirdle)
                && CompareArrayString(target.Inclusion.NaturalOnGirdle, filter.NaturalOnGirdle)
                && CompareArrayString(target.Inclusion.NaturalOnCrown, filter.NaturalOnCrown)
                && CompareArrayString(target.Inclusion.NaturalOnPavillion, filter.NaturalOnPavillion)
                && CompareArrayString(target.Inclusion.NaturalOnTable, filter.NaturalOnTable)
                && CompareArrayString(target.Inclusion.HNA, filter.HNA)
                && CompareArrayString(target.Inclusion.EyeClean, filter.EyeClean)
                && CompareArrayString(target.Inclusion.EFOC, filter.EFOC)
                && CompareArrayString(target.Inclusion.EFOP, filter.EFOP)
                && CompareArrayString(target.Inclusion.EFOG, filter.EFOG)
                && CompareArrayString(target.Inclusion.EFOT, filter.EFOT)
                && CompareArrayString(target.Inclusion.GirdleCondition, filter.GirdleCondition)
                && CompareArrayString(target.Inclusion.Luster, filter.Luster)
                && CompareArrayString(target.Inclusion.Bowtie, filter.Bowtie);
        }

        public bool FilterTempPricingData(TempPricing target, InventorySearchCriteria filter) {
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

            List<string> stoneIds = new List<string>();
            if (filter.StockId != null) {
                if (filter.StockId.Contains(","))
                    stoneIds = filter.StockId.Split(",").ToList();
                else if (filter.StockId.Contains(" "))
                    stoneIds = filter.StockId.Split(" ").ToList();
                else
                    stoneIds.Add(filter.StockId);
            }

            var inv = _repository.GetOne<InventoryItems>(z => z.StoneId == target.StoneId);

            if (inv != null)
                return (filter.Carat == null || (range.Any(c => c.MinWeight <= inv.Weight && c.MaxWeight >= inv.Weight)))
                    && (filter.FromDate == null || filter.ToDate == null || ((filter.FromDate != null && filter.ToDate != null) && filter.FromDate?.Date <= inv.CreatedDate.Date && filter.ToDate?.Date >= inv.CreatedDate.Date))
                    && CheckCommonString(inv.StoneOrg.BranchName, filter.Branch)
                    && CheckCommonString(inv.StoneOrg.DeptId, filter.DeptId)
                    && CheckCommonString(inv.Identity.Id, filter.EmpId)
                    && CheckCommonString(inv.CertificateNo, filter.CertificateNo)
                    && CheckCommonString(inv.Kapan, filter.Kapan)
                    && CheckCommonBoolen(inv.IsHold, filter.IsHold)
                    && CheckCommonBoolen(inv.IsRapnetHold, filter.IsRapnetHold)
                    && CheckFromToNegativeDecimalValues(inv.BasePrice.Discount, filter.FromDiscount, filter.ToDiscount)
                    && CheckFromToDecimalValues(inv.Measurement.Ratio, filter.FromRatio, filter.ToRatio)
                    && CheckFromToDecimalValues(inv.Measurement.Depth, filter.FromDepth, filter.ToDepth)
                    && CheckFromToDecimalValues(inv.Measurement.Table, filter.FromTable, filter.ToTable)
                    && CheckFromToDecimalValues(inv.Measurement.Length, filter.FromLength, filter.ToLength)
                    && CheckFromToDecimalValues(inv.Measurement.Width, filter.FromWidth, filter.ToWidth)
                    && CheckFromToDecimalValues(inv.Measurement.Height, filter.FromHeight, filter.ToHeight)
                    && CheckFromToDecimalValues(inv.Measurement.CrownHeight, filter.FromCrownHeight, filter.ToCrownHeight)
                    && CheckFromToDecimalValues(inv.Measurement.CrownAngle, filter.FromCrownAngle, filter.ToCrownAngle)
                    && CheckFromToDecimalValues(inv.Measurement.PavilionDepth, filter.FromPavilionDepth, filter.ToPavilionDepth)
                    && CheckFromToDecimalValues(inv.Measurement.PavilionAngle, filter.FromPavilionAngle, filter.ToPavilionAngle)
                    && CheckFromToDecimalValues(inv.Measurement.GirdlePer, filter.FromGirdlePer, filter.ToGirdlePer)
                    && CheckFromToDecimalValues(inv.BasePrice.PerCarat, filter.FromPerCarat, filter.ToPerCarat)
                    && CheckFromToDecimalValues(inv.BasePrice.NetAmount, filter.FromNetAmt, filter.ToNetAmt)
                    && CompareArrayString(inv.StoneId, stoneIds)
                    && CompareArrayString(inv.Location, filter.Location)
                    && CompareArrayString(inv.Status, filter.Status)
                    && CompareArrayString(inv.Shape, filter.Shape)
                    && CompareArrayString(inv.Color, filter.Color)
                    && CompareArrayString(inv.Clarity, filter.Clarity)
                    && CompareArrayString(inv.Fluorescence, filter.Flour)
                    && CompareArrayString(inv.Inclusion.Shade, filter.Shade)
                    && CompareArrayString(inv.Lab, filter.Lab)
                    && CompareArrayString(inv.Cut, filter.Cut)
                    && CompareArrayString(inv.Polish, filter.Polish)
                    && CompareArrayString(inv.Symmetry, filter.Symm)
                    && CompareArrayString(inv.Inclusion.KtoS, filter.KToS)
                    && CompareArrayString(inv.Inclusion.Culet, filter.Culet)
                    && CompareArrayString(inv.Inclusion.SideBlack, filter.SideBlack)
                    && CompareArrayString(inv.Inclusion.CenterBlack, filter.CenterBlack)
                    && CompareArrayString(inv.Inclusion.SideWhite, filter.SideWhite)
                    && CompareArrayString(inv.Inclusion.CenterWhite, filter.CenterWhite)
                    && CompareArrayString(inv.Inclusion.Brown, filter.Brown)
                    && CompareArrayString(inv.Inclusion.Green, filter.Green)
                    && CompareArrayString(inv.Inclusion.Milky, filter.Milky)
                    && CompareArrayString(inv.Inclusion.OpenCrown, filter.OpenCrown)
                    && CompareArrayString(inv.Inclusion.OpenTable, filter.OpenTable)
                    && CompareArrayString(inv.Inclusion.OpenPavilion, filter.OpenPavilion)
                    && CompareArrayString(inv.Inclusion.OpenGirdle, filter.OpenGirdle)
                    && CompareArrayString(inv.Inclusion.NaturalOnGirdle, filter.NaturalOnGirdle)
                    && CompareArrayString(inv.Inclusion.NaturalOnCrown, filter.NaturalOnCrown)
                    && CompareArrayString(inv.Inclusion.NaturalOnPavillion, filter.NaturalOnPavillion)
                    && CompareArrayString(inv.Inclusion.NaturalOnTable, filter.NaturalOnTable)
                    && CompareArrayString(inv.Inclusion.HNA, filter.HNA)
                    && CompareArrayString(inv.Inclusion.EyeClean, filter.EyeClean)
                    && CompareArrayString(inv.Inclusion.EFOC, filter.EFOC)
                    && CompareArrayString(inv.Inclusion.EFOP, filter.EFOP)
                    && CompareArrayString(inv.Inclusion.EFOG, filter.EFOG)
                    && CompareArrayString(inv.Inclusion.EFOT, filter.EFOT)
                    && CompareArrayString(inv.Inclusion.GirdleCondition, filter.GirdleCondition)
                    && CompareArrayString(inv.Inclusion.Luster, filter.Luster)
                    && CompareArrayString(inv.Inclusion.Bowtie, filter.Bowtie);

            return false;
        }

        public bool CompareArrayString(string target, List<string> filter) {
            bool result = true;

            if (filter != null && filter.Count > 0 && target != null && target.Length > 0)
                return filter.Contains(target, StringComparer.CurrentCultureIgnoreCase);

            return result;
        }

        public bool CheckFromToDecimalValues(decimal? target, decimal? filterFrom, decimal? filterTo) {
            bool result = true;

            if ((filterFrom != null && filterFrom > 0) && (filterTo != null && filterTo > 0) && target != null && target > 0)
                return (filterFrom <= target && filterTo >= target);

            return result;
        }

        public bool CheckFromToNegativeDecimalValues(decimal? target, decimal? filterFrom, decimal? filterTo) {
            bool result = true;

            if (filterFrom != null && filterTo != null && target != null)
                return (filterFrom <= target && filterTo >= target);

            return result;
        }

        public bool CheckCommonString(string target, string filter) {
            bool result = true;

            if (filter != null && filter.Length > 0 && target != null && target.Length > 0)
                return (target.ToLower() == filter.ToLower());

            return result;
        }

        public bool CheckCommonBoolen(bool? target, bool? filter) {
            bool result = true;

            if (filter != null && target != null)
                return (target == filter);

            return result;
        }
    }
}
