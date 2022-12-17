using Utilities.DTOs;
using Utilities.Interfaces;

namespace Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(MailgunSettingDTO settings
            , string fromName
            , string subject
            , string body
            , IEnumerable<string> toAddresses
            , IEnumerable<string> ccAddresses = null
            , IEnumerable<string> bccAddresses = null
            , string from = null
            , string replyMessageId = null
            , bool isBodyHtml = true)
        {
            throw new NotImplementedException();
        }
    }
}
