using ICaNer.Shared.DTOs.Product;

namespace ICaNet.ApplicationCore.Interfaces
{
    public interface IProductService
    {
        Task<List<GetProductResponse>> GetAllProduct(string userId,int pageSize = 10, string filter = "", int itemSkip = 0);
        Task<bool> AddProductAsync(AddProductRequest addProduct, string userId);
        Task<bool> DeleteProductAsync(DeletProductRequest deletProductRequest, string userId);
        Task<EditeProductResponse> EditeProductAsync(EditeProductRequest editeProduct, string userId);
    }
}
