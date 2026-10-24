using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Configuration.DNorm;
using System.Collections.Generic;

namespace CRM.Entities.MongoEntities.Configuration {
    public class MasterConfig : Document {
        public IList<MasterDNorm> Lab { get; set; }
        public IList<MasterDNorm> Shape { get; set; }
        public IList<MasterDNorm> Colors { get; set; }
        public IList<MasterDNorm> Clarities { get; set; }
        public IList<MasterDNorm> CPS { get; set; }
        public IList<MasterDNorm> Fluorescence { get; set; }
        public IList<MasterDNorm> Cut { get; set; }
        public IList<CutDetailDNorm> CutDetails { get; set; }
        public IList<MasterDNorm> Inclusions { get; set; }
        public InclusionConfig InclusionConfig { get; set; }
        public IList<MasterDNorm> Measurements { get; set; }
        public MeasurementConfig MeasurementConfig { get; set; }

        public MasterConfig() {
            Lab = new List<MasterDNorm>();
            Shape = new List<MasterDNorm>();
            Colors = new List<MasterDNorm>();
            Clarities = new List<MasterDNorm>();
            CPS = new List<MasterDNorm>();
            Fluorescence = new List<MasterDNorm>();
            Cut = new List<MasterDNorm>();
            CutDetails = new List<CutDetailDNorm>();
            Inclusions = new List<MasterDNorm>();
            InclusionConfig = new InclusionConfig();
            Measurements = new List<MasterDNorm>();
            MeasurementConfig = new MeasurementConfig();
        }
    }
}
