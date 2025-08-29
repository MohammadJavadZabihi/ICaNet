using ICaNet.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ICaNet.ApplicationCore.Entities.Products
{
    public class UnitOfMeasurement : BaseEntity, IAggregateRoot
    {
        public UnitOfMeasurement()
        {
            
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Symbol { get; set; }

        #region Relations

        public ICollection<Product> Products { get; set; }

        #endregion
    }
}
