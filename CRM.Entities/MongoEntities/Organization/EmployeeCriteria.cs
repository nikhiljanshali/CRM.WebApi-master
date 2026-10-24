using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Entities.MongoEntities.Organization {
    public class EmployeeCriteria : Document {
        public EmployeeDNorm Employee { get; set; }
        public IList<string> Shape { get; set; }
        public IList<string> Lab { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Min Weight is required!")]
        public decimal MinWeight { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Max Weight is required!")]
        public decimal MaxWeight { get; set; }

        public IList<string> Color { get; set; }
        public IList<string> Clarity { get; set; }
        public IList<string> Cut { get; set; }
        public IList<string> Polish { get; set; }
        public IList<string> Symmetry { get; set; }
        public IList<string> Fluorescence { get; set; }
        public int? MinDay { get; set; }
        public int? MaxDay { get; set; }
        public IList<string> Organizations { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Up Limit is required!")]
        public decimal UpLimit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Down Limit is required!")]
        public decimal DownLimit { get; set; }

        public EmployeeCriteria() {
            Employee = new EmployeeDNorm();
            Shape = new List<string>();
            Lab = new List<string>();
            Color = new List<string>();
            Clarity = new List<string>();
            Cut = new List<string>();
            Polish = new List<string>();
            Symmetry = new List<string>();
            Fluorescence = new List<string>();
            Organizations = new List<string>();
        }
    }
}
