using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs.User;
using ICaNet.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ICaNet.API.EndPoints.UserEndpoints;

[Route("api/v{version:apiversion}/user")]
[ApiVersion("1.0")]
[Authorize]
public class UserProfileEndpoint : EndpointBaseAsync.WithoutRequest.WithActionResult
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UserProfileEndpoint(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("GetDashboard")]
    public async override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var response = new UserGetDashboardResponse();

        var userReq = HttpContext.User;
        if(!userReq.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        string userId = userReq.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                response.PhoneNumber = user.PhoneNumber;
                response.Family = user.UserFamily;
                response.TotalExpenses = 0;
                response.PositiveProfit = 0;
                response.NegativeProfit = 0;
                response.Email = user.Email;
                response.UserName = user.UserName;
                response.Address = user.UserAddress;

                return Ok(response);
            }
        }

        return Unauthorized();
    }
}

