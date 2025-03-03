using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs.Product;
using ICaNet.ApplicationCore.Interfaces;
using ICaNet.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ICaNet.API.EndPoints.ProductEndpoints;

[Route("api/v{version:apiversion}/Product")]
[ApiVersion("1.0")]
[Authorize]
public class ProductDeletEndpoint : EndpointBaseAsync.WithRequest<DeletProductRequest>.WithResult<DeleteProductRespone>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IProductService _productService;
    public ProductDeletEndpoint(UserManager<ApplicationUser> userManager,
        IProductService productService)
    {
        _userManager = userManager;
        _productService = productService;
    }

    [HttpDelete("Delete")]
    public async override Task<DeleteProductRespone> HandleAsync(DeletProductRequest request, CancellationToken cancellationToken = default)
    {
        var response = new DeleteProductRespone();

        var userRequest = HttpContext.User;
        if (!userRequest.Identity.IsAuthenticated || userRequest == null)
        {
            response.Result = false;
            response.Messgae = "خطای احراز حویت";

            return response;
        }

        string? userId = userRequest.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            response.Result = false;
            response.Messgae = "خطای احراز حویت";

            return response;
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            response.Result = false;
            response.Messgae = "کاربری یافت نشد";

            return response;
        }

        var deletProduct = await _productService.DeleteProductAsync(request, userId);

        if(!deletProduct)
        {
            response.Result = false;
            response.Messgae = "خطا در حذف کالا";

            return response;
        }

        response.Result = true;
        response.Messgae = "کالا با موقیت حذف گردید";

        return response;
    }
}

