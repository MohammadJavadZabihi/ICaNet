using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.Infrastructure.Identity
{
    public class WrongInfoUserForRegister : Exception
    {
        public WrongInfoUserForRegister() : base("Cannot regsiter user. Pleas review the information!")
        {
            
        }
    }
}
