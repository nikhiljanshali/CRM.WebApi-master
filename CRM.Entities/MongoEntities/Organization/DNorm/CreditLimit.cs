namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class CreditLimit {
        public decimal? Amount { get; set; }
        public int? Days { get; set; }

        public CreditLimit() { }
        public CreditLimit(decimal? amount, int? days) {
            this.Amount = amount;
            this.Days = days;
        }
    }
}