using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CRM.IdentityServer.Model {
    public class IndexModel {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public string AuthenticatorKey { get; set; }

        public IndexModel() { }
    }
}
