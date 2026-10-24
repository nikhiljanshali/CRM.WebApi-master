using CRM.Core.MongoCore.Model;

namespace CRM.Entities.MongoEntities.Lab {
    public class LabResult : Document {
        public string LabName { get; set; }
        public string JobNo { get; set; }
        public string ControlNo { get; set; }
        public string DiamondDossier { get; set; }
        public string ReportNo { get; set; }
        public string ReportDt { get; set; }
        public string ClientRef { get; set; }
        public string MemoNo { get; set; }
        public string Shape { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Depth { get; set; }
        public decimal? Weight { get; set; }
        public string Color { get; set; }
        public string ColorDescriptions { get; set; }
        public string Clarity { get; set; }
        public string ClarityStatus { get; set; }
        public string FinalCut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string FluorescenceIntensity { get; set; }
        public string FluorescenceColor { get; set; }
        public string Girdle { get; set; }
        public string GirdleCondition { get; set; }
        public string CuletSize { get; set; }
        public decimal? DepthPer { get; set; }
        public decimal? TablePer { get; set; }
        public decimal? CrnAg { get; set; }
        public string CrnHt { get; set; }
        public decimal? PavAg { get; set; }
        public string PavDp { get; set; }
        public string StrLn { get; set; }
        public string LrHalf { get; set; }
        public string Painting { get; set; }
        public string Proportion { get; set; }
        public string PaintComm { get; set; }
        public string KeytoSymbols { get; set; }
        public string ReportComments { get; set; }
        public string Inscription { get; set; }
        public string SyntheticIndicator { get; set; }
        public string GirdlePer { get; set; }
        public string PolishFeatures { get; set; }
        public string SymmetryFeatures { get; set; }
        public string ShapeDescription { get; set; }
        public string ReportType { get; set; }
        public string Sorting { get; set; }
        public string BasketStatus { get; set; }
        public string DocumentNo { get; set; }
        public string Prefix { get; set; }
        public string OtherReportNumber { get; set; }
        public string NumberStones { get; set; }
        public string Description { get; set; }
        public string ReportTypeI { get; set; }
        public string ReportFormatI { get; set; }
        public string ReportTypeII { get; set; }
        public string ReportFormatII { get; set; }
        public string CommentsConsultation { get; set; }
        public string SpecialComments { get; set; }
        public string FaxComments { get; set; }
        public string Recheck { get; set; }
        public string EmailTransitOurRef { get; set; }
        public string Done { get; set; }
        public string PotentialClarity { get; set; }
        public string WeightlossClarity { get; set; }
        public string ECG { get; set; }
        public string DiamLWMinmm { get; set; }
        public string DiamLWMaxmm { get; set; }
        public string HeightCAvgmm { get; set; }
        public string CrownPer { get; set; }
        public string PavillionPer { get; set; }
        public string Hna { get; set; }
        public string PotentialMake { get; set; }
        public string Remarks { get; set; }
        public string PremiumIdealCut { get; set; }

        public LabResult() { }

    }
}
