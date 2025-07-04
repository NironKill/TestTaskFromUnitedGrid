using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.Infrastructure.Common.Models;
using Notification.Infrastructure.Email.Options;

namespace Notification.Infrastructure.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOption _option;

        public EmailService(IOptions<EmailOption> option)
        {
            _option = option.Value;
        }
        public async Task SendEmail(NotificationSendDTO dto, CancellationToken cancellationToken)
        {
            using (SmtpClient client = new SmtpClient())
            {
                foreach (KeyValuePair<string, HashSet<string>> notificationBatch in dto.UnreadMessages)
                {
                    MimeMessage mail = new MimeMessage();
                    mail.Sender = MailboxAddress.Parse(_option.EmailSender);
                    mail.To.AddRange(notificationBatch.Value.Select(email => MailboxAddress.Parse(email)));
                    BodyBuilder builder = new BodyBuilder();
                    builder.HtmlBody = notificationBatch.Key;
                    mail.Body = builder.ToMessageBody();

                    client.Connect(_option.SmtpServer, _option.SmtpPort, SecureSocketOptions.StartTls);
                    client.Authenticate(_option.EmailSender, _option.Password);
                    await client.SendAsync(mail, cancellationToken);
                }
            }     
        }
    }
}
