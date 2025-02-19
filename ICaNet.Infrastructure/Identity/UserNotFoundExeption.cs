using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.Infrastructure.Identity
{
    public class UserNotFoundExeption : Exception
    {
        public UserNotFoundExeption(string userName) : base($"No user found with username : {userName}")
        {
            
        }
    }
}
