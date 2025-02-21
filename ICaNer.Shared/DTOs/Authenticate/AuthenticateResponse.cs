using ICaNer.Shared.Models;

namespace ICaNer.Shared.DTOs.Authenticate
{
    public class AuthenticateResponse : BaseResponse
    {
        public AuthenticateResponse(Guid correlationId) : base(correlationId) { }

        public AuthenticateResponse() { }

        public bool Result { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool IsNotAllowed { get; set; } = false;
    }
}
