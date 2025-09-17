using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;


namespace TechStore.Services
{
    public class EmailService : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromAddress = new MailAddress("techstore4200@gmail.com");
            var toAddress = new MailAddress(email);
            const string fromPassword = "keqsxjmsjkqbjzbd"; // Hasło do skrzynki emailowej (lepiej zabezpieczyć, np. z konfiguracji)

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // Serwer SMTP, np. dla Gmail: smtp.gmail.com
                Port = 587, // Port dla Gmail: 587
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true // Ustawienie na true, jeśli chcesz wysłać HTML
            };

            await smtp.SendMailAsync(mailMessage);
        }
    }
}
