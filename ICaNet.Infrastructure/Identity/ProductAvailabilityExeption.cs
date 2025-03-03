using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.Infrastructure.Identity
{
    public class ProductAvailabilityExeption : Exception
    {
        public ProductAvailabilityExeption(string productName, string productCod) : base($"محصول با نام {productName} و کد {productCod} موجود است")
        {
            
        }
    }
}
