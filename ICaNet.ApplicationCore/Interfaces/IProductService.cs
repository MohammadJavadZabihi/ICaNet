using ICaNer.Shared.DTOs.Product;
using ICaNet.ApplicationCore.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.ApplicationCore.Interfaces
{
    public interface IProductService
    {
        Task<List<GetProductResponse>> GetAllProduct(string userId,int pageSize = 10, string filter = "", int itemSkip = 0);
        Task<bool> AddProductAsync(AddProductRequest addProduct, string userId);
    }
}
