using CRM.Core.MongoCore.Model;

namespace CRM.Entities.MongoEntities.Pricing {
    public class RapPrice : Document {
        public string Shape { get; set; }
        public decimal MinSize { get; set; }
        public decimal MaxSize { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public decimal Price { get; set; }

        public RapPrice() { }
    }
}
