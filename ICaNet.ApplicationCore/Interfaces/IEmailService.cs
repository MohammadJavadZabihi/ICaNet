using ICaNer.Shared.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.ApplicationCore.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(SendEmailDTO sendEmail);
        Task ConfrimEmail(string userId, string token);
    }
}
