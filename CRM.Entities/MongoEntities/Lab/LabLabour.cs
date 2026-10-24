namespace CRM.Entities.MongoEntities.Lab {
    public class LabLabour {
        public string Type { get; set; }
        public decimal MinSize { get; set; }
        public decimal MaxSize { get; set; }
        public double Amount { get; set; }
        public bool IsPerPcs { get; set; }        

        public LabLabour() { }

    }
}
