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
        public int Id { get; set; }
        public string Name { get; set; }
        public double Count { get; set; }
        public double Price { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Code { get; set; } 
        public string Statuce { get; set; }
        public string Supplier { get; set; }

    }
}
