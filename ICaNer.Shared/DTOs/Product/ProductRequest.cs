using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.Product
{
    public class ProductRequest : BaseRequest
    {
        public int ItemSkip { get; set; }
        public string Filter { get; set; } = string.Empty;
        public int PageSize { get; set; }
    }
}
