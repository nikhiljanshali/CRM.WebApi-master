using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Registration {
    /// <summary>
    /// Customer Registration object
    /// </summary>
    public class RegisterCustomer : Document{
        //Personal Information
        [Required(ErrorMessage ="Name is requiered!")]
        public Name Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Full name is required!")]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "BirthDate is required!")]
        public DateTime BirthDate { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Email is required!")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is not valid!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Country for phone is required!")]
        public string CountryCode { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Mobile number is required!")]
        public string PrimaryMobile { get; set; }
        public string SecondaryMobile { get; set; }
        public string PhoneNo { get; set; }
        
        //Business Information
        [Required(AllowEmptyStrings =false,ErrorMessage ="Company name is required!")]
        public string CompanyName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Designation is required!")]
        public string Designation { get; set; }
        public string BusinessType { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessMobileNo { get; set; }
        public string BusinessPhoneNo { get; set; }
        public Address Address { get; set; }
        public string FaxNo { get; set; }
        //Referal
        public string ReferenceName { get; set; }
        
        //Social profiles
        public IList<SocialMedia> SocialMedias { get; set; }
        public string Status { get; set; }

        public RegisterCustomer() {
            Name = new Name();
            Address = new Address();
            SocialMedias = new List<SocialMedia>();
            Status = StatusType.InProgress.ToString();
        }
    }
}