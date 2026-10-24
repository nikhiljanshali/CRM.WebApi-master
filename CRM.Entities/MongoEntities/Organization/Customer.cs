using CRM.Core.Common;
using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Common;
using CRM.Entities.MongoEntities.Organization.DNorm;
using CRM.Entities.MongoEntities.Registration.DNorm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Organization {
    public class Customer : Document {        
        public Name Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Full name is required!")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "BirthDate is required!")]
        public DateTime BirthDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Origin is required!")]
        [DataType(DataType.Text)]
        public string Origin { get; set; }
        public Badge Badge { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Country for phone is required!")]
        public string CountryCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile no is required!")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string PhoneNo { get; set; }

        //Business Information
        [Required(AllowEmptyStrings = false, ErrorMessage = "Company name is required!")]
        public string CompanyName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Designation is required!")]
        public string Designation { get; set; }
        public string BusinessType { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessMobileNo { get; set; }
        public string BusinessPhoneNo { get; set; }
        public Address Address { get; set; }
        public string FaxNo { get; set; }
        public string IncomeTaxNo { get; set; }
        public CreditLimit CreditLimit { get; set; }
        public OrganizationDNorm Organization { get; set; }
        public BranchDNorm Branch { get; set; }
        public string ReferenceName { get; set; }
        public IList<SocialMedia> SocialMedias { get; set; }
        public SellerDNorm Seller { get; set; }
        public decimal Discount { get; set; }

        public Customer() {
            Name = new Name();
            Badge = new Badge();
            Address = new Address();
            CreditLimit = new CreditLimit();
            Organization = new OrganizationDNorm();
            Branch = new BranchDNorm();
            SocialMedias = new List<SocialMedia>();
            Seller = new SellerDNorm();
            Origin = OriginType.Customer.ToString();
        }

    }
}