using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Common {
    public class Name {
        [Required(AllowEmptyStrings =false,ErrorMessage ="First name is required!")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Last name is required!")]
        public string LastName { get; set; }

        public Name() { }
        public Name(string firstName, string lastName) : this(firstName, "", lastName) { }
        public Name(string firstName, string middleName, string lastName) {
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
        }

    }
}