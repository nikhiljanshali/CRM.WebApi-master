using System.Threading.Tasks;

namespace CRM.IdentityServer.Services {
    public interface ISmsSender {
        Task SendSmsAsync(string number, string message);
    }
}
