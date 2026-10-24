using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.MarketSheets {
    public class MarketSheetSellerSearchCriteria {
        public string OrganizationId { get; set; }
        public string Branch { get; set; }
        public string DeptId { get; set; }
        public string EmpId { get; set; }
        public string StockId { get; set; }
        public string CertificateNo { get; set; }
        public string Carat { get; set; }
        public List<string> Location { get; set; }
        public List<string> Status { get; set; }
        public List<string> Shape { get; set; }
        public List<string> Color { get; set; }
        public List<string> Clarity { get; set; }
        public List<string> Flour { get; set; }
        public List<string> Shade { get; set; }
        public List<string> Lab { get; set; }
        public List<string> Cut { get; set; }
        public List<string> Polish { get; set; }
        public List<string> Symm { get; set; }
        public List<string> KToS { get; set; }
        public List<string> Culet { get; set; }
        public List<string> Brown { get; set; }
        public List<string> Green { get; set; }
        public List<string> Milky { get; set; }
        public List<string> SideBlack { get; set; }
        public List<string> CenterBlack { get; set; }
        public List<string> SideWhite { get; set; }
        public List<string> CenterWhite { get; set; }
        public List<string> OpenCrown { get; set; }
        public List<string> OpenTable { get; set; }
        public List<string> OpenPavilion { get; set; }
        public List<string> OpenGirdle { get; set; }
        public List<string> NaturalOnGirdle { get; set; }
        public List<string> NaturalOnCrown { get; set; }
        public List<string> NaturalOnPavillion { get; set; }
        public List<string> NaturalOnTable { get; set; }
        public List<string> HNA { get; set; }
        public List<string> EyeClean { get; set; }
        public List<string> EFOC { get; set; }
        public List<string> EFOP { get; set; }
        public List<string> EFOT { get; set; }
        public List<string> EFOG { get; set; }
        public List<string> GirdleCondition { get; set; }
        public List<string> Luster { get; set; }
        public List<string> Bowtie { get; set; }
        public decimal? FromRatio { get; set; }
        public decimal? ToRatio { get; set; }
        public decimal? FromDepth { get; set; }
        public decimal? ToDepth { get; set; }
        public decimal? FromTable { get; set; }
        public decimal? ToTable { get; set; }
        public decimal? FromLength { get; set; }
        public decimal? ToLength { get; set; }
        public decimal? FromWidth { get; set; }
        public decimal? ToWidth { get; set; }
        public decimal? FromHeight { get; set; }
        public decimal? ToHeight { get; set; }
        public decimal? FromCrownHeight { get; set; }
        public decimal? ToCrownHeight { get; set; }
        public decimal? FromCrownAngle { get; set; }
        public decimal? ToCrownAngle { get; set; }
        public decimal? FromPavilionAngle { get; set; }
        public decimal? ToPavilionAngle { get; set; }
        public decimal? FromPavilionDepth { get; set; }
        public decimal? ToPavilionDepth { get; set; }
        public decimal? FromGirdlePer { get; set; }
        public decimal? ToGirdlePer { get; set; }
        public decimal? FromPerCarat { get; set; }
        public decimal? ToPerCarat { get; set; }
        public decimal? FromNetAmt { get; set; }
        public decimal? ToNetAmt { get; set; }
        public bool? IsHold { get; set; }
        public bool? IsOnMemo { get; set; }
        public bool? IsInLead { get; set; }
        public bool? IsRapnetHold { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public MarketSheetSellerSearchCriteria() {
            this.Location = new List<string>();
            this.Status = new List<string>();
            this.Shape = new List<string>();
            this.Lab = new List<string>();
            this.Cut = new List<string>();
            this.Polish = new List<string>();
            this.Symm = new List<string>();
            this.KToS = new List<string>();
            this.Culet = new List<string>();
            this.Brown = new List<string>();
            this.Green = new List<string>();
            this.Milky = new List<string>();
            this.SideBlack = new List<string>();
            this.CenterBlack = new List<string>();
            this.SideWhite = new List<string>();
            this.CenterWhite = new List<string>();
            this.OpenCrown = new List<string>();
            this.OpenTable = new List<string>();
            this.OpenPavilion = new List<string>();
            this.OpenGirdle = new List<string>();
            this.NaturalOnGirdle = new List<string>();
            this.NaturalOnCrown = new List<string>();
            this.NaturalOnPavillion = new List<string>();
            this.NaturalOnTable = new List<string>();
            this.HNA = new List<string>();
            this.EyeClean = new List<string>();
            this.EFOC = new List<string>();
            this.EFOP = new List<string>();
            this.EFOG = new List<string>();
            this.EFOT = new List<string>();
            this.GirdleCondition = new List<string>();
            this.Luster = new List<string>();
            this.Bowtie = new List<string>();
        }
    }
}
