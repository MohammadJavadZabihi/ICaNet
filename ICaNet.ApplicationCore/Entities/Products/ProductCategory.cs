using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICaNet.ApplicationCore.Entities.Products
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            
        }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }


        #region Realtions

        [ForeignKey("ProductId")]
        public Product Product{ get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        #endregion
    }
}
