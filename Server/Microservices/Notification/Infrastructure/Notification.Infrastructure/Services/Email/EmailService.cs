using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.Infrastructure.Options;

namespace Notification.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailOption _option;

        public EmailService(IOptions<EmailOption> option)
        {
            _option = option.Value;
        }
        public async Task SendEmail(string recipient, string message)
        {
            using SmtpClient client = new SmtpClient();

            var mail = new MimeMessage();
            mail.Sender = MailboxAddress.Parse(_option.EmailSender);
            mail.To.Add(MailboxAddress.Parse(recipient));
            BodyBuilder builder = new BodyBuilder();
            builder.HtmlBody = message;
            mail.Body = builder.ToMessageBody();

            client.Connect(_option.SmtpServer, _option.SmtpPort, SecureSocketOptions.StartTls);
            client.Authenticate(_option.EmailSender, _option.Password);
            await client.SendAsync(mail);       
        }
    }
}
