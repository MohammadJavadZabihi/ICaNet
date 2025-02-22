using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.Email
{
    public class EmailConfrimResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
