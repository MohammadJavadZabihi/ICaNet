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
public class AddProductEndpoint : EndpointBaseAsync.WithRequest<AddProductRequest>.WithResult<AddProductResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IProductService _productService;
    public AddProductEndpoint(UserManager<ApplicationUser> userManager,
        IProductService productService)
    {
        _userManager = userManager;
        _productService = productService;
    }

    [HttpPost("Add")]
    public async override Task<AddProductResponse> HandleAsync(AddProductRequest request, CancellationToken cancellationToken = default)
    {
        var response = new AddProductResponse();

        var userRequest = HttpContext.User;
        if (!userRequest.Identity.IsAuthenticated || userRequest == null)
        {
            response.Result = false;
            response.Message = "خطای سطح دسترسی";

            return response;
        }

        string? userId = userRequest.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            response.Result = false;
            response.Message = "خطای سطح دسترسی";

            return response;
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            response.Result = false;
            response.Message = "خطای سطح دسترسی";

            return response;
        }

        var addProduct = await _productService.AddProductAsync(request, userId);

        if(!addProduct)
        {
            response.Result = false;
            response.Message = "خطا, لطفا در ارسال اطلاعات دقت فرمایید";

            return response;
        }

        response.Result = true;
        response.Message = "کالای جدید با موفقیت ثبت شد";

        return response;
    }
}

