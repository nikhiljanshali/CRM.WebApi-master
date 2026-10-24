using MessageBird;
using MessageBird.Objects;

namespace CRM.Core.OmniChannel {
    public class BaseClient : IBaseClient {
        private readonly Client client;

        public BaseClient(string accessKey) {
            client = Client.CreateDefault(accessKey);
        }

        public Message SendRecoveryOTPSMS(long phoneNum, string code) {
            string body = code + " is your SECRET one Time Password (OTP) for Password Recovery. Do not share it with anyone.";

            var message = client.SendMessage("recursivediamond", body, new[] { phoneNum });
            return message;
        }

    }
}
