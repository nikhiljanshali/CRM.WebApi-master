namespace CRM.Entities.MongoEntities.Configuration.Base {
    public class IssueRight {
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public bool IsAutoIssue { get; set; }
        public bool IsManuallyIssue { get; set; }

        public IssueRight() { }
    }
}
