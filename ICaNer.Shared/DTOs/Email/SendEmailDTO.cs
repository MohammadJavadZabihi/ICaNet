using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.Email
{
    public class SendEmailDTO
    {
        public SendEmailDTO(string email, string subject, string message)
        {
            Email = email;
            Subject = subject;
            Message = message;
        }

        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
