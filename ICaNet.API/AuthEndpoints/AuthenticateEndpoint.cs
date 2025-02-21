using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNet.ApplicationCore.Interfaces;
using ICaNet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ICaNet.API.AuthEndpoints;

[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class AuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<AuthenticateRequest>
    .WithResult<AuthenticateResponse>
{
    private readonly SignInManager<ApplicationUser> _userManager;
    private readonly ITokenClaimsService _tokenClaimsService;
    public AuthenticateEndpoint(SignInManager<ApplicationUser> userManager,
        ITokenClaimsService tokenClaimsService)
    {
        _tokenClaimsService = tokenClaimsService;
        _userManager = userManager;
    }

    [HttpPost("authenticate")]
    public override async Task<AuthenticateResponse> HandleAsync(AuthenticateRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = new AuthenticateResponse(request.CoorelationId());

        var result = await _userManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

        response.Result = result.Succeeded;
        response.Username = request.UserName;
        response.IsNotAllowed = result.IsNotAllowed;

        if (result.Succeeded)
        {
            response.Token = await _tokenClaimsService.GetTokenAsync(request.UserName);
        }

        return response;
    }
}

