using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Inventory {
    public class InclusionExcelItems {
        public string StoneId { get; set; }
        //InventoryItemMeasurement
        public decimal? Depth { get; set; }
        public decimal? Table { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? CrownHeight { get; set; }
        public decimal? CrownAngle { get; set; }
        public decimal? PavilionDepth { get; set; }
        public decimal? PavilionAngle { get; set; }
        public decimal? GirdlePer { get; set; }
        public string MinGirdle { get; set; }
        public string MaxGirdle { get; set; }
        public decimal? Ratio { get; set; }
        //InventoryItemInclusion
        public string Brown { get; set; }
        public string Green { get; set; }
        public string Milky { get; set; }
        public string Shade { get; set; }
        public string CenterBlack { get; set; } //TableBlack
        public string SideBlack { get; set; } //CrownBlack
        public string OpenCrown { get; set; }
        public string OpenTable { get; set; }
        public string OpenPavilion { get; set; }
        public string OpenGirdle { get; set; }
        public string GirdleCondition { get; set; }
        public string EFOC { get; set; }
        public string EFOP { get; set; }
        public string Culet { get; set; }
        public string HNA { get; set; }
        public string EyeClean { get; set; }
        public string NaturalOnGirdle { get; set; }
        public string NaturalOnCrown { get; set; }
        public string NaturalOnPavillion { get; set; }
        public string Bowtie { get; set; }
        public string UpdatedBy { get; set; }
    }
}
