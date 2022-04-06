using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace SapConnectionTest.Services.Mail
{
    public class MailService : MailArguments
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
            var mailSettings = _configuration.GetSection("MailSettings").Get<MailArguments>();

            MailFrom = mailSettings.MailFrom;
            MailTo = mailSettings.MailTo;
            Bcc = mailSettings.Bcc;
            Subject = mailSettings.Subject;
            Body = mailSettings.Body;
            Attachment = mailSettings.Attachment;
            IsBodyHtml = mailSettings.IsBodyHtml;
            Name = mailSettings.Name;
            Username = mailSettings.Username;
            Password = mailSettings.Password;
            SmtpHost = mailSettings.SmtpHost;
            Port = mailSettings.Port;
            EnableSsl = mailSettings.EnableSsl;
        }

        internal void SendMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Name, MailFrom));
            message.To.Add(new MailboxAddress(MailTo, MailTo));
            message.Subject = Subject;
            message.Body = new TextPart("html")
            {
                Text = Body
            };

            var smtpClient = new SmtpClient();
            try
            {
                smtpClient.Connect(SmtpHost, Port, EnableSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None);
                if (Username != "")
                {
                    smtpClient.Authenticate(Username, Password);
                }
                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
            finally
            {
                smtpClient.Disconnect(true);
            }
        }
    }
}
