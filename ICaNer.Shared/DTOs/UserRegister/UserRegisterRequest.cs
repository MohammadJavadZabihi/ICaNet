using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.UserRegister
{
    public class UserRegisterRequest : BaseRequest
    {
        public bool RegisterWithPhone { get; set; } = false;
        public string Name { get; set; }
        public string Family { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string RePassword { get; set; }
        public string Address { get; set; }
    }
}
