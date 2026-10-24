using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Organization.DNorm {
    public class Department {
        public string Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Name is required!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Origin is required!")]
        public string Origin { get; set; }
        public string BranchName { get; set; }
        public string PhoneExtNo { get; set; }

        public Department() { }

    }
}