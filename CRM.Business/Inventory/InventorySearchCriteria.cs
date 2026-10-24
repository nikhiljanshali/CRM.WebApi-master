using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Inventory {
    public class InventorySearchCriteria {
        public string OrganizationId { get; set; }
        public string Branch { get; set; }
        public string DeptId { get; set; }
        public string EmpId { get; set; }
        public string StockId { get; set; }
        public string Kapan { get; set; }
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
        public decimal? FromDiscount { get; set; }
        public decimal? ToDiscount { get; set; }
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
        //public decimal? FromGirdle { get; set; }
        //public decimal? ToGirdle { get; set; }
        public decimal? FromGirdlePer { get; set; }
        public decimal? ToGirdlePer { get; set; }
        public decimal? FromPerCarat { get; set; }
        public decimal? ToPerCarat { get; set; }
        public decimal? FromNetAmt { get; set; }
        public decimal? ToNetAmt { get; set; }

        public bool? IsHold { get; set; }
        public bool? IsRapnetHold { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public InventorySearchCriteria() {
            Location = new List<string>();
            Status = new List<string>();
            Shape = new List<string>();
            Lab = new List<string>();
            Cut = new List<string>();
            Polish = new List<string>();
            Symm = new List<string>();
            KToS = new List<string>();
            Culet = new List<string>();
            Brown = new List<string>();
            Green = new List<string>();
            Milky = new List<string>();
            SideBlack = new List<string>();
            CenterBlack = new List<string>();
            SideWhite = new List<string>();
            CenterWhite = new List<string>();
            OpenCrown = new List<string>();
            OpenTable = new List<string>();
            OpenPavilion = new List<string>();
            OpenGirdle = new List<string>();
            NaturalOnGirdle = new List<string>();
            NaturalOnCrown = new List<string>();
            NaturalOnPavillion = new List<string>();
            NaturalOnTable = new List<string>();
            HNA = new List<string>();
            EyeClean = new List<string>();
            EFOC = new List<string>();
            EFOP = new List<string>();
            EFOG = new List<string>();
            EFOT = new List<string>();
            GirdleCondition = new List<string>();
            Luster = new List<string>();
            Bowtie = new List<string>();
        }
    }
}