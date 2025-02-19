using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string UserFamily { get; set; }
        public string UserAddress { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionExpiryDate { get; set; }
    }
}
