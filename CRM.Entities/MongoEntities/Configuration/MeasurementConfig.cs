namespace CRM.Entities.MongoEntities.Configuration {
    public class MeasurementConfig {
        public bool Depth { get; set; }
        public bool Table { get; set; }
        public bool Length { get; set; }
        public bool Width { get; set; }
        public bool Height { get; set; }
        public bool CrownHeight { get; set; }
        public bool CrownAngle { get; set; }
        public bool PavillionAngle { get; set; }
        public bool PavillionDepth { get; set; }
        public bool GirdlePer { get; set; }
        public bool Girdle { get; set; }
        public bool Ratio { get; set; }

        public MeasurementConfig() { }
    }
}
