using ICaNet.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICaNet.ApplicationCore.Entities.Products
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public Product()
        {
            
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public double Count { get; set; }

        [Required]
        public int UnitOfMeasurementId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(450)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Statuce { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        #region Relations 

        public ICollection<ProductCategory> ProductCategories { get; set; }


        [ForeignKey("SupplierId")]
        public Supplier SuppLier { get; set; }


        [ForeignKey("UnitOfMeasurementId")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        #endregion
    }
}
