namespace SapConnectionTest.Services.Mail
{
    public class MailArguments
    {
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
