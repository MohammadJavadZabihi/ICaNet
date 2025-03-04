using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.Product
{
    public class AddProductResponse:BaseResponse
    {
        public bool Result { get; set; } = false;
        public string Message { get; set; }
    }
}
