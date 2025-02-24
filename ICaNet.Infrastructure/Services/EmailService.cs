using ICaNet.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ICaNer.Shared.DTOs.Email;
using Microsoft.AspNetCore.Identity;
using ICaNet.Infrastructure.Identity;

namespace ICaNet.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ConfrimEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new UserNotFoundExeption("");

            if (user.EmailConfirmed) return;

            var confrimEmail = await _userManager.ConfirmEmailAsync(user, token);


        }

        public async Task SendEmailAsync(SendEmailDTO sendEmail)
        {
            var emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "ConfirmEmail.html");
            var emailBody = await File.ReadAllTextAsync(emailTemplatePath);
            emailBody = emailBody.Replace("{{confirmationLink}}", sendEmail.Message);

            var mailMessage = new MailMessage
            {
                From = new MailAddress("Hots@HostMail.ir", "حسابداری آیکانت"),
                Body = emailBody,
                Subject = sendEmail.Subject,
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(sendEmail.Email));

            using (var smtpClient = new SmtpClient("webmail.HostDoiman.ir", 587))
            {
                smtpClient.Credentials = new NetworkCredential("Hots@HostMail.ir", "Password");
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
