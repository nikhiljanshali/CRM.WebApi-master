namespace CRM.Business.Inventory {
    public class InventoryExcelItems {
        public string StoneId { get; set; }
        public string Kapan { get; set; }
        public string Article { get; set; }
        public string Shape { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public decimal? Rap { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PerCarat { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        //InvItemMeasurement
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

        //StoneOrgDNorm
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
        public string BranchName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        //IdentityOrgDNorm
        public string EmpId { get; set; }
        public string EmpName { get; set; }

        public InventoryExcelItems() { }
    }
}
