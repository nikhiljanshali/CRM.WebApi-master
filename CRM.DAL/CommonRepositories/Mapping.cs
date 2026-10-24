using CRM.Business.Inventory;
using CRM.DAL.BaseRepositories;
using CRM.Entities.MongoEntities.Inventory;
using CRM.Entities.MongoEntities.Organization;
using CRM.Entities.MongoEntities.Organization.DNorm;
using CRM.Entities.MongoEntities.Pricing;

namespace CRM.DAL.CommonRepositories {
    public class Mapping {
        public readonly IMongoRepository _repository;

        public Mapping(IMongoRepository repository) {
            _repository = repository;
        }

        public PricingHistory MappingInvToPricingHistory(InventoryItems inventory) {
            PricingHistory pricing = new PricingHistory();
            pricing.StoneId = inventory.StoneId;
            pricing.Shape = inventory.Shape;
            pricing.Color = inventory.Color;
            pricing.Clarity = inventory.Clarity;
            pricing.Cut = inventory.Cut;
            pricing.Polish = inventory.Polish;
            pricing.Symmetry = inventory.Symmetry;
            pricing.Fluorescence = inventory.Fluorescence;
            pricing.Weight = inventory.Weight;

            if (inventory.BasePrice != null)
                pricing.Price = inventory.BasePrice;

            pricing.CreatedBy = inventory.CreatedBy;
            pricing.UpdatedBy = inventory.UpdatedBy;

            if (inventory.StoneOrg != null && inventory.StoneOrg.OrgId != null && inventory.StoneOrg.OrgId.Length > 0)
                pricing.Organization = _repository.ProjectOne<Organization, OrganizationDNorm>(c => c.Id == inventory.StoneOrg.OrgId,
                    j => new OrganizationDNorm {
                        Id = j.Id,
                        Name = j.Name,
                        Person = j.Person,
                        PhoneNo = j.PhoneNo,
                        Email = j.Email
                    });

            return pricing;
        }

        public InventoryItems MappingInvExcelToInvItems(InventoryExcelItems inv, string identType) {
            InventoryItems invItem = new InventoryItems();
            invItem.StoneId = inv.StoneId;
            invItem.Article = inv.Article;
            invItem.Shape = inv.Shape;
            invItem.Weight = inv.Weight;
            invItem.Color = inv.Color;
            invItem.Clarity = inv.Clarity;
            invItem.Cut = inv.Cut;
            invItem.Polish = inv.Polish;
            invItem.Symmetry = inv.Symmetry;
            invItem.Fluorescence = inv.Fluorescence;
            invItem.Comments = inv.Comments;
            invItem.Kapan = inv.Kapan;
            invItem.Status = inv.Status;

            //Rap
            invItem.BasePrice.Rap = inv.Rap;
            invItem.BasePrice.Discount = inv.Discount;
            invItem.BasePrice.NetAmount = inv.NetAmount;
            invItem.BasePrice.PerCarat = inv.PerCarat;

            //Measurement
            invItem.Measurement.Depth = inv.Depth;
            invItem.Measurement.Table = inv.Table;
            invItem.Measurement.Length = inv.Length;
            invItem.Measurement.Width = inv.Width;
            invItem.Measurement.Height = inv.Height;
            invItem.Measurement.CrownHeight = inv.CrownHeight;
            invItem.Measurement.CrownAngle = inv.CrownAngle;
            invItem.Measurement.PavilionAngle = inv.PavilionAngle;
            invItem.Measurement.PavilionDepth = inv.PavilionDepth;
            invItem.Measurement.GirdlePer = inv.GirdlePer;
            invItem.Measurement.MinGirdle = inv.MinGirdle;
            invItem.Measurement.MaxGirdle = inv.MaxGirdle;
            invItem.Measurement.Ratio = inv.Ratio;

            //Stone Organization
            invItem.StoneOrg.OrgId = inv.OrgId;
            invItem.StoneOrg.OrgName = inv.OrgName;
            invItem.StoneOrg.DeptId = inv.DeptId;
            invItem.StoneOrg.DeptName = inv.DeptName;
            invItem.StoneOrg.BranchName = inv.BranchName;
            invItem.StoneOrg.Country = inv.Country;
            invItem.StoneOrg.City = inv.City;

            //Identity
            invItem.Identity.Id = inv.EmpId;
            invItem.Identity.Name = inv.EmpName;
            invItem.Identity.Type = identType;

            //Document
            invItem.CreatedBy = inv.EmpId;
            invItem.UpdatedBy = inv.EmpId;

            return invItem;
        }

        public InventoryItems MappingIncExcelToInvItems(InclusionExcelItems inc, InventoryItems inv) {
            //Measurement
            if (inc.Depth != null && inc.Table != null && inc.Length != null && inc.Width != null && inc.Height != null && inc.CrownHeight != null && inc.CrownAngle != null &&
                inc.PavilionAngle != null && inc.PavilionDepth != null && inc.GirdlePer != null && inc.MinGirdle != null && inc.MaxGirdle != null && inc.Ratio != null) {
                inv.Measurement.Depth = inc.Depth;
                inv.Measurement.Table = inc.Table;
                inv.Measurement.Length = inc.Length;
                inv.Measurement.Width = inc.Width;
                inv.Measurement.Height = inc.Height;
                inv.Measurement.CrownHeight = inc.CrownHeight;
                inv.Measurement.CrownAngle = inc.CrownAngle;
                inv.Measurement.PavilionAngle = inc.PavilionAngle;
                inv.Measurement.PavilionDepth = inc.PavilionDepth;
                inv.Measurement.GirdlePer = inc.GirdlePer;
                inv.Measurement.MinGirdle = inc.MinGirdle;
                inv.Measurement.MaxGirdle = inc.MaxGirdle;
                inv.Measurement.Ratio = inc.Ratio;
            }

            //Inclusion
            inv.Inclusion.Brown = inc.Brown;
            inv.Inclusion.Green = inc.Green;
            inv.Inclusion.Milky = inc.Milky;
            inv.Inclusion.Shade = inc.Shade;
            inv.Inclusion.SideBlack = inc.SideBlack;
            inv.Inclusion.CenterBlack = inc.CenterBlack;
            inv.Inclusion.OpenCrown = inc.OpenCrown;
            inv.Inclusion.OpenTable = inc.OpenTable;
            inv.Inclusion.OpenPavilion = inc.OpenPavilion;
            inv.Inclusion.OpenGirdle = inc.OpenGirdle;
            inv.Inclusion.GirdleCondition = inc.GirdleCondition;
            inv.Inclusion.EFOC = inc.EFOC;
            inv.Inclusion.EFOP = inc.EFOP;
            inv.Inclusion.Culet = inc.Culet;
            inv.Inclusion.HNA = inc.HNA;
            inv.Inclusion.EyeClean = inc.EyeClean;
            inv.Inclusion.NaturalOnGirdle = inc.NaturalOnGirdle;
            inv.Inclusion.NaturalOnCrown = inc.NaturalOnCrown;
            inv.Inclusion.NaturalOnPavillion = inc.NaturalOnPavillion;
            inv.Inclusion.Bowtie = inc.Bowtie;

            inv.UpdatedBy = inc.UpdatedBy;

            return inv;
        }

        public TempPricing MappingInvToTempPricing(InventoryItems inventory) {
            TempPricing target = new TempPricing();

            //target.Id = inventory.Id;
            target.StoneId = inventory.StoneId;
            target.Kapan = inventory.Kapan;
            target.Article = inventory.Article;
            target.Grade = inventory.Grade;
            target.Shape = inventory.Shape;
            target.Weight = inventory.Weight;
            target.Color = inventory.Color;
            target.Clarity = inventory.Clarity;
            target.Cut = inventory.Cut;
            target.Polish = inventory.Polish;
            target.Symmetry = inventory.Symmetry;
            target.Fluorescence = inventory.Fluorescence;
            target.Lab = inventory.Lab;
            target.Comments = inventory.Comments;
            target.VideoUrl = inventory.VideoUrl;
            target.ImageUrl = inventory.ImageUrl;
            target.CertificateUrl = inventory.CertificateUrl;
            target.BasePrice = inventory.BasePrice;
            target.TempPrice = new PriceDNorm();
            target.Inclusion = inventory.Inclusion;
            target.Measurement = inventory.Measurement;
            target.StoneOrg = inventory.StoneOrg;
            target.Identity = inventory.Identity;

            return target;
        }

        public PricingRequest MappingInvToPricingRequest(InventoryItems inventory, string status) {
            PricingRequest target = new PricingRequest();

            target.StoneId = inventory.StoneId;
            target.Kapan = inventory.Kapan;
            target.Article = inventory.Article;
            target.Grade = inventory.Grade;
            target.Shape = inventory.Shape;
            target.Weight = inventory.Weight;
            target.Color = inventory.Color;
            target.Clarity = inventory.Clarity;
            target.Cut = inventory.Cut;
            target.Polish = inventory.Polish;
            target.Symmetry = inventory.Symmetry;
            target.Fluorescence = inventory.Fluorescence;
            target.Lab = inventory.Lab;
            target.Comments = inventory.Comments;
            target.VideoUrl = inventory.VideoUrl;
            target.ImageUrl = inventory.ImageUrl;
            target.CertificateUrl = inventory.CertificateUrl;
            target.BasePrice = inventory.BasePrice;
            target.TempPrice = new PriceDNorm();
            target.Inclusion = inventory.Inclusion;
            target.Measurement = inventory.Measurement;
            target.StoneOrg = inventory.StoneOrg;
            target.Identity = inventory.Identity;
            target.Status = status;

            return target;
        }

        public PricingRequest MappingTempPricingToPricingRequest(TempPricing pricing, string status) {
            PricingRequest target = new PricingRequest();

            target.StoneId = pricing.StoneId;
            target.Kapan = pricing.Kapan;
            target.Article = pricing.Article;
            target.Grade = pricing.Grade;
            target.Shape = pricing.Shape;
            target.Weight = pricing.Weight;
            target.Color = pricing.Color;
            target.Clarity = pricing.Clarity;
            target.Cut = pricing.Cut;
            target.Polish = pricing.Polish;
            target.Symmetry = pricing.Symmetry;
            target.Fluorescence = pricing.Fluorescence;
            target.Lab = pricing.Lab;
            target.Comments = pricing.Comments;
            target.VideoUrl = pricing.VideoUrl;
            target.ImageUrl = pricing.ImageUrl;
            target.CertificateUrl = pricing.CertificateUrl;
            target.BasePrice = pricing.BasePrice;
            target.TempPrice = pricing.TempPrice;
            target.Inclusion = pricing.Inclusion;
            target.Measurement = pricing.Measurement;
            target.StoneOrg = pricing.StoneOrg;
            target.Identity = pricing.Identity;
            target.Status = status;

            return target;
        }

        public StockOnHandHistory MappingInvToStockOnHandHistory(InventoryItems inv) {
            var previousInv = _repository.GetOne<InventoryItems>(c => c.StoneId == inv.StoneId);

            StockOnHandHistory stone = new StockOnHandHistory();

            stone.StoneId = inv.StoneId;
            stone.Kapan = inv.Kapan;
            stone.Article = inv.Article;
            stone.RFID = inv.RFID;
            stone.Grade = inv.Grade;
            stone.Shape = inv.Shape;
            stone.Weight = inv.Weight;
            stone.Color = inv.Color;
            stone.Clarity = inv.Clarity;
            stone.Cut = inv.Cut;
            stone.Polish = inv.Polish;
            stone.Symmetry = inv.Symmetry;
            stone.Fluorescence = inv.Fluorescence;
            stone.Location = inv.Location;
            stone.Status = inv.Status;
            stone.Lab = inv.Lab;
            stone.CertificateNo = inv.CertificateNo;
            stone.IsHold = inv.IsHold;

            //BasePrice
            stone.BasePrice.Rap = inv.BasePrice.Rap;
            stone.BasePrice.Discount = inv.BasePrice.Discount;
            stone.BasePrice.NetAmount = inv.BasePrice.NetAmount;
            stone.BasePrice.PerCarat = inv.BasePrice.PerCarat;

            //Identity
            stone.FromIdentity.Id = previousInv.Identity.Id; //From result Old Values
            stone.FromIdentity.Name = previousInv.Identity.Name; //From result Old Values
            stone.FromIdentity.Type = previousInv.Identity.Type; //From result Old Values

            stone.ToIdentity.Id = inv.Identity.Id;
            stone.ToIdentity.Name = inv.Identity.Name;
            stone.ToIdentity.Type = inv.Identity.Type;

            //Document
            stone.UpdatedBy = inv.UpdatedBy;
            stone.UpdatedAt = inv.UpdatedAt;

            return stone;
        }
    }
}
