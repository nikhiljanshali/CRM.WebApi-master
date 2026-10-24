using CRM.Entities.MongoEntities.SystemUserNameSpace;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Business.SystemUsers {
    public class SystemUserResponse {
        public List<SystemUser> SystemUsers;
        public long TotalCount;

        public SystemUserResponse() {
            SystemUsers = new List<SystemUser>();
        }
    }
}
