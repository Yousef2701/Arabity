using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace ArabityAuth.Code
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host= "smtp.gmail.com",
                EnableSsl= true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials= false,
                Credentials = new NetworkCredential("your email sender","password")
            };

            return client.SendMailAsync("your email sender",email,subject,htmlMessage);
        }
    }
}
