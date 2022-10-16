using System.Threading.Tasks;

namespace IdentityAPI.Models
{
    public interface IEmailService
    {
        Task SendEmail(string to,string subject,string body);

    }
}