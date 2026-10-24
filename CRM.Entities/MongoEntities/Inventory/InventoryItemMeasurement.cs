namespace CRM.Entities.MongoEntities.Inventory {
    public class InventoryItemMeasurement {
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

        public InventoryItemMeasurement() { }

    }
}