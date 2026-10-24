using CRM.Entities.MongoEntities.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.Organizations
{
   public class OrganizationResponse
    {
        public List<Organization> Organizations;
        public long TotalCount;
        public OrganizationResponse()
        {
            Organizations = new List<Organization>();
        }
    }
}