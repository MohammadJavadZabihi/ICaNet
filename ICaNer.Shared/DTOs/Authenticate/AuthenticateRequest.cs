using ICaNer.Shared.Models;

namespace ICaNer.Shared.DTOs.Authenticate
{
    public class AuthenticateRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
