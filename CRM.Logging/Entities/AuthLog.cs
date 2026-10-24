using CRM.Core.MongoCore.Model;

namespace CRM.Logging.Entities {
    public class AuthLog : Document {
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public string UserIP { get; set; }
        public string Browser { get; set; }
        public string Event { get; set; }

        public AuthLog() { }
        public AuthLog(string clientId, string userId, string userIp, string browser, string events) {
            this.ClientId = clientId;
            this.UserId = userId;
            this.UserIP = userIp;
            this.Browser = browser;
            this.Event = events;
            this.Version = 1;
        }
    }
}
