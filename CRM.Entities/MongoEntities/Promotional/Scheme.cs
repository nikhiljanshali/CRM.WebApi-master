using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Promotional.DNorm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Promotional {
    public class Scheme : Document {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Name is required!")]
        public string Name { get; set; }

        public string Image { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string[] Origin { get; set; }

        public IList<SchemeDetail> Details { get; set; }

        public bool IsActive { get; set; }

        public Scheme() {
            Details = new List<SchemeDetail>();
        }

    }
}