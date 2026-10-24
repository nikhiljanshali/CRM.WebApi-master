namespace CRM.Core.OmniChannel {
    public class OTPModel {
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Code { get; set; }

        public OTPModel() { }
        public OTPModel(string userName, string mobileNo) {
            this.UserName = userName;
            this.MobileNo = mobileNo;
        }
    }
}
