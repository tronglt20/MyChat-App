using Utilities.DTOs;

namespace Utilities.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailgunSettingDTO settings
            , string fromName
            , string subject
            , string body
            , IEnumerable<string> toAddresses
            , IEnumerable<string> ccAddresses = null
            , IEnumerable<string> bccAddresses = null
            , string from = null
            , string replyMessageId = null
            , bool isBodyHtml = true
        );
    }
}
