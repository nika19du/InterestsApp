using System.Collections.Generic;
using System.Threading.Tasks;

namespace InYourInterest.Services.Providers
{
    public interface IEmailSender
    {
        Task SendEmailAsync(
            string from,
            string fromName,
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
