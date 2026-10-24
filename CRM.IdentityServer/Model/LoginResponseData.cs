using System;

namespace CRM.IdentityServer.Model {
    public class LoginResponseData {
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public int Expires_in { get; set; }
        public string UserName { get; set; }
        public string Ident { get; set; }
        public string Code { get; set; }
        public string Origin { get; set; }
        public string Token_type { get; set; }
        public string Client_id { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
        public bool ForceChangePassword { get; set; }

        public LoginResponseData() { }
    }
}
