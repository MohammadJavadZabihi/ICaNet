using ICaNet.ApplicationCore.Entities.Products;
using ICaNet.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ICaNet.ApplicationCore.Entities.Pepole
{
    public class Person : BaseEntity, IAggregateRoot
    {
        public Person()
        {
            
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(350)]
        public string EmailAddress { get; set; }

        #region Relations

        public ICollection<PersonalFinaces> PersonalFinaces { get; set; }
        public ICollection<Product> Products { get; set; }

        #endregion
    }
}
