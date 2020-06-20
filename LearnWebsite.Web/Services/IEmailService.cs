using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email message
        /// </summary>
        /// <param name="to">The address to send the email to</param>
        /// <param name="subject">The email message title</param>
        /// <param name="htmlBody">The html content of the message</param>
        public Task SendEmailAsync(string to, string subject, string htmlBody);

        /// <summary>
        /// Updates the current smtp configuration
        /// </summary>
        public void UpdateConfiguration();
    }
}
