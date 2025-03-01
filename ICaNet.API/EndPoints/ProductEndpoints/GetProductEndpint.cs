using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs.Product;
using ICaNet.ApplicationCore.Interfaces;
using ICaNet.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ICaNet.API.EndPoints.ProductEndpoints
{
    [Route("api/v{version:apiversion}/Product")]
    [ApiVersion("1.0")]
    [Authorize]
    public class GetProductEndpint : EndpointBaseAsync.WithRequest<ProductRequest>.WithActionResult<List<GetProductResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        public GetProductEndpint(UserManager<ApplicationUser> userManager,
            IProductService productService)
        {
            _userManager = userManager;
            _productService = productService;
        }

        [HttpPost("GetAll")]
        public override async Task<ActionResult<List<GetProductResponse>>> HandleAsync(ProductRequest request, CancellationToken cancellationToken = default)
        {
            var userRequest = HttpContext.User;
            if (userRequest == null || !userRequest.Identity.IsAuthenticated)
            {
                return Unauthorized("خطای دسترسی");
            }

            string? userId = userRequest.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("کاربری یافت نشد");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized("کاربری یافت نشد");
            }

            var response = await _productService.GetAllProduct(userId, request.PageNumber, request.PageSize, request.Filter);

            return Ok(response);
        }
    }
}
