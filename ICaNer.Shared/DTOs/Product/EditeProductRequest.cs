using ICaNer.Shared.Models;

namespace ICaNer.Shared.DTOs.Product
{
    public class EditeProductRequest : BaseRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string Statuce { get; set; }
    }
}
