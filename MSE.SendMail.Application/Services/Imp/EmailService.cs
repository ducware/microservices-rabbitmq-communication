using MailKit.Net.Smtp;
using MimeKit;
using MSE.SendMail.Application.Configurations;
using MSE.SendMail.Domain.Models;

namespace MSE.SendMail.Application.Services.Imp
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigs _emailConfigs;
        public EmailService(EmailConfigs emailConfigs)
        {
            _emailConfigs = emailConfigs;
        }

        public void SendEmail(Message message)
        {
            var email = CreateEmailMessage(message);
            Send(email);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("email", _emailConfigs.From));
            email.To.AddRange(message.To);
            email.Subject = message.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return email;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();

            try
            {
                client.Connect(_emailConfigs.SmtpServer, _emailConfigs.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfigs.Username, _emailConfigs.Password);

                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
