using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.Product
{
    public class GetProductResponse : BaseResponse
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Code { get; set; } 
        public string Statuce { get; set; }

    }
}
