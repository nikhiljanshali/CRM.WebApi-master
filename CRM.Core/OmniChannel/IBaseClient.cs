using MessageBird.Objects;

namespace CRM.Core.OmniChannel {
    public interface IBaseClient {
        Message SendRecoveryOTPSMS(long phoneNum, string code);
    }
}
