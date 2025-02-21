using ICaNet.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Product> Products { get; set; }

        #endregion
    }
}
