using ICaNet.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.ApplicationCore.Entities.Products
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public Category()
        {
            
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        #region Realtions

        public ICollection<ProductCategory> ProductCategories { get; set; }

        #endregion
    }
}
