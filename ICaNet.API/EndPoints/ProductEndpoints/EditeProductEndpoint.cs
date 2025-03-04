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
public class EditeProductEndpoint : EndpointBaseAsync.WithRequest<EditeProductRequest>.WithResult<EditeProductResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IProductService _productService;

    public EditeProductEndpoint(UserManager<ApplicationUser> userManager,
        IProductService productService)
    {
        _productService = productService;
        _userManager = userManager;
    }

    [HttpPut("Update")]
    public async override Task<EditeProductResponse> HandleAsync(EditeProductRequest request, CancellationToken cancellationToken = default)
    {
        var response = new EditeProductResponse();

        var userRequest = HttpContext.User;

        if(userRequest == null || !userRequest.Identity.IsAuthenticated)
        {
            response.Result = false;
            response.Message = "خطای سطح دسترسی کاربر";

            return response;
        }

        string? userId = userRequest.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(string.IsNullOrEmpty(userId))
        {
            response.Result = false;
            response.Message = "خطای سطح دسترسی کاربر";

            return response;
        }

        var editeProductResponse = await _productService.EditeProductAsync(request, userId);

        return editeProductResponse;
    }
}

