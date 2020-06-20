using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfigurationSection _smtpConfig;
        private SmtpClient client;

        public EmailService(IConfiguration configuration)
        {
            _smtpConfig = configuration.GetSection("smtp");
            UpdateConfiguration();
        }

        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var message = new MailMessage(_smtpConfig["from"], to, subject, htmlBody);
            message.IsBodyHtml = true;
            await client.SendMailAsync(message);
        }

        public void UpdateConfiguration()
        {
            if (client != null) client.Dispose(); // quit old client
            client = new SmtpClient(_smtpConfig["server"], int.Parse(_smtpConfig["port"]));
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_smtpConfig["user"], _smtpConfig["password"]);
        }
    }
}
