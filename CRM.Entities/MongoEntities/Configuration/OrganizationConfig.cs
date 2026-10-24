using CRM.Core.MongoCore.Model;
using CRM.Entities.MongoEntities.Organization.DNorm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Entities.MongoEntities.Configuration {
    public class OrganizationConfig : Document {
        public OrganizationDNorm OrganizationDNorm { get; set; }
        public string ApiPath { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string CertificateUrl { get; set; }

        public OrganizationConfig() {
            OrganizationDNorm = new OrganizationDNorm();
        }
    }
}
