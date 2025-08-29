using ICaNet.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICaNet.ApplicationCore.Entities.Pepole
{
    public class PersonalFinaces : BaseEntity, IAggregateRoot
    {
        public PersonalFinaces()
        {
            
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public long AmountOwed { get; set; }

        [Required]
        public long AmountOfClaim { get; set; }

        public DateTime LastPay { get; set; }

        #region Relations

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        #endregion
    }
}
