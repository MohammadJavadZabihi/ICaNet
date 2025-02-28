using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.User
{
    public class UserGetDashboardResponse : BaseResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double PositiveProfit { get; set; }
        public double NegativeProfit { get; set; }
        public double TotalIncome { get; set; }
        public double TotalExpenses { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
