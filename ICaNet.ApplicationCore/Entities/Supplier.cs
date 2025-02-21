using ICaNet.ApplicationCore.Entities.Products;
using ICaNet.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ICaNet.ApplicationCore.Entities
{
    public class Supplier : BaseEntity, IAggregateRoot
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [MaxLength(800)]
        [Required]
        public string PhysicalAddress { get; set; }

        [Required]
        public double RemainingAmount { get; set; }

        [Required]
        public string Statuce { get; set; }

        [MaxLength(900)]
        public string TaxNumber { get; set; }

        #region Realtions

        public List<Product> Products { get; set; }

        #endregion
    }
}
