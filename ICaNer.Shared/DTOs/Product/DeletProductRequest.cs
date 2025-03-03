using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.Product
{
    public class DeletProductRequest : BaseRequest
    {
        public string ProduName { get; set; }
        public string ProductCode { get; set; }
    }
}
