using ICaNer.Shared.Models;

namespace ICaNer.Shared.DTOs.Product
{
    public class DeleteProductRespone : BaseResponse
    {
        public bool Result { get; set; }
        public string Messgae { get; set; }
    }
}
