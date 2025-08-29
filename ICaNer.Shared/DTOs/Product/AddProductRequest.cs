using ICaNer.Shared.Models;

namespace ICaNer.Shared.DTOs.Product
{
    public class AddProductRequest : BaseRequest
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string UnitOfMeasurementName { get; set; }
        public long Price { get; set; }
        public string Code { get; set; }
        public string SupplierName { get; set; }
        public string Statuce { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
