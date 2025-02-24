using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.UserRegister
{
    public class UserRegisterResponse : BaseResponse
    {
        public bool Result { get; set; } = false;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
