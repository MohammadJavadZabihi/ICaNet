using ICaNer.Shared.DTOs.Product;
using ICaNet.ApplicationCore.Entities.Products;
using ICaNet.ApplicationCore.Interfaces;
using ICaNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ICaNet.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly CoreDbContext _coreDbContext;
        public ProductService(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }
        public async Task<List<GetProductResponse>> GetAllProduct(string userId, int pageNumber = 1, int pageSize = 10, string filter = "")
        {
            IQueryable<Product> productResponses = _coreDbContext.Products
                .Include(p => p.UnitOfMeasurement)
                .Where(p => p.UserId == userId);

            if(!string.IsNullOrEmpty(filter))
            {
                productResponses = productResponses.Where(p=> p.Name.Contains(filter) || p.Code.Contains(filter));
            }

            var skip = (pageNumber - 1) * pageSize;

            var products = await productResponses
                .Skip(skip)
                .Take(pageSize)
                .Select(p => new GetProductResponse
                {
                    Name = p.Name,
                    Code = p.Code,
                    Count = p.Count,
                    Statuce = p.Statuce,
                    UnitOfMeasurement = p.UnitOfMeasurement.Name

                })
                .ToListAsync();

            return products;
        }
    }
}
