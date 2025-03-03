using ICaNer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNer.Shared.DTOs.Product
{
    public class DeleteProductRespone : BaseResponse
    {
        public bool Result { get; set; }
        public string Messgae { get; set; }
    }
}
