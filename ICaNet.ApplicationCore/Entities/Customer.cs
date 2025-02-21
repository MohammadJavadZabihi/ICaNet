using ICaNet.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.ApplicationCore.Entities
{
    public class Customer : BaseEntity, IAggregateRoot
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Statuce { get; set; }

        [Required]
        [MaxLength(250)]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(1000)]
        public string PhysicalAddress { get; set; }

        [Required]
        public double RemainingAmount { get; set; }
    }
}
