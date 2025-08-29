using ICaNer.Shared.Models;

namespace ICaNer.Shared.DTOs
{
    public class ResponseDto : BaseResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
