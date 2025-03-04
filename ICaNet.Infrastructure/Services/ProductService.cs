using ICaNer.Shared.DTOs.Product;
using ICaNet.ApplicationCore.Entities;
using ICaNet.ApplicationCore.Entities.Products;
using ICaNet.ApplicationCore.Interfaces;
using ICaNet.Infrastructure.Data;
using ICaNet.Infrastructure.Identity;
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

        public async Task<bool> AddProductAsync(AddProductRequest addProduct, string userId)
        {
            var isExistProduct = await _coreDbContext.Products.AnyAsync(p => p.Name == addProduct.Name && p.Code == addProduct.Code);

            if (isExistProduct)
            {
                return false;
            }

            using var transaction = await _coreDbContext.Database.BeginTransactionAsync();

            try
            {
                var supplier = await _coreDbContext.Suppliers
                                    .FirstOrDefaultAsync(s => s.Name == addProduct.SupplierName);

                if (supplier == null)
                {
                    supplier = new Supplier
                    {
                        EmailAddress = "ندارد",
                        Name = addProduct.SupplierName,
                        PhoneNumber = "ندارد",
                        PhysicalAddress = "ندارد",
                        RemainingAmount = 0,
                        Statuce = "نامشخص",
                        TaxNumber = "ندارد",
                        UserId = userId
                    };

                    await _coreDbContext.Suppliers.AddAsync(supplier);
                    await _coreDbContext.SaveChangesAsync();


                }

                var unitOfMeasurement = await _coreDbContext.UnitOfMeasurement
                                            .FirstOrDefaultAsync(u => u.Name == addProduct.UnitOfMeasurementName);

                if (unitOfMeasurement == null)
                {
                    unitOfMeasurement = new UnitOfMeasurement
                    {
                        Symbol = "ندارد",
                        UserId = userId,
                        Name = addProduct.UnitOfMeasurementName
                    };

                    await _coreDbContext.UnitOfMeasurement.AddAsync(unitOfMeasurement);
                    await _coreDbContext.SaveChangesAsync();

                }

                var newProduct = new Product
                {
                    Code = addProduct.Code,
                    Count = addProduct.Count,
                    Name = addProduct.Name,
                    Price = addProduct.Price,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Statuce = addProduct.Statuce,
                    SupplierId = supplier.Id,
                    UnitOfMeasurementId = unitOfMeasurement.Id,
                    UserId = userId
                };

                await _coreDbContext.Products.AddAsync(newProduct);

                await _coreDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(DeletProductRequest deletProductRequest, string userId)
        {
            try
            {
                var product = await _coreDbContext.Products
                    .FirstOrDefaultAsync(p => p.Name == deletProductRequest.ProduName && 
                    p.Code == deletProductRequest.ProductCode &&
                    p.UserId == userId);

                if (product == null)
                {
                    return false;
                }

                _coreDbContext.Products.Remove(product);
                await _coreDbContext.SaveChangesAsync();

                return true;    
            }
            catch
            {
                return false;
            }
        }

        public async Task<EditeProductResponse> EditeProductAsync(EditeProductRequest editeProduct, string userId)
        {
            var response = new EditeProductResponse();

            var product = await _coreDbContext.Products.FirstOrDefaultAsync(p => p.Id == editeProduct.Id && p.UserId == userId);

            if(product == null)
            {
                response.Result = false;
                response.Message = "کالا وجود ندارد";

                return response;
            }

            var isExistProduct = await _coreDbContext.Products.AnyAsync(p => 
            p.Name == editeProduct.Name && p.Code == editeProduct.Code && p.UserId == userId);

            if (isExistProduct)
            {
                response.Result = false;
                response.Message = "این کالا با این مشخصات موجود است";

                return response;
            }

            product.Code = editeProduct.Code;
            product.Count = editeProduct.Count;
            product.Name = editeProduct.Name;
            product.Price = editeProduct.Price;
            product.Statuce = editeProduct.Statuce;

            _coreDbContext.Products.Update(product);
            await _coreDbContext.SaveChangesAsync();


            response.Result = true;
            response.Message = "کالا با موفقیت ویرایش شد";

            return response;

        }

        public async Task<List<GetProductResponse>> GetAllProduct(string userId, int pageSize = 10, string filter = "", int itemSkip = 0)
        {
            IQueryable<Product> productResponses = _coreDbContext.Products
                .Include(p => p.UnitOfMeasurement)
                .Where(p => p.UserId == userId);

            if (!string.IsNullOrEmpty(filter))
            {
                productResponses = productResponses.Where(p => p.Name.Contains(filter) || p.Code.Contains(filter));
            }

            var products = await productResponses
                .Skip(itemSkip)
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
