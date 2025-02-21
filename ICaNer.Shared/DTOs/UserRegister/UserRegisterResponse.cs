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
        public string Message { get; set; } = string.Empty;
    }
}
