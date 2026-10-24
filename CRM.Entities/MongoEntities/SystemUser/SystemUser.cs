using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.MongoEntities.SystemUserNameSpace {
    public class SystemUser : Document {
        public Name Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Full name is required!")]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Origin is required!")]
        public string Origin { get; set; }

        public Badge Badge { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required!")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid mobile number")]
        public string Mobile { get; set; }
        public Address Address { get; set; }
        public string NativeAddress { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EnrollmentNumber { get; set; }
        public bool IsManager { get; set; }
        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }
        public IList<SocialMedia> SocialMedias { get; set; }

        public SystemUser() {
            Name = new Name();
            Badge = new Badge();
            Address = new Address();
            SocialMedias = new List<SocialMedia>();
        }

    }
}