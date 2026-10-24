using System.Collections.Generic;

namespace CRM.IdentityServer.Model {
    public class SendCodeModel {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }

        public SendCodeModel() { }
    }
}
