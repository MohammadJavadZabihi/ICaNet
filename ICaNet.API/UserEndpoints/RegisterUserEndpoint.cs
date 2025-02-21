using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs;
using ICaNer.Shared.DTOs.UserRegister;
using ICaNet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ICaNet.API.UserEndpoints;

[Route("api/v{version:apiversion}")]
[ApiVersion("1.0")]
public class RegisterUserEndpoint : EndpointBaseAsync
    .WithRequest<UserRegisterRequest>
    .WithResult<UserRegisterResponse>
{

    private readonly UserManager<ApplicationUser> _userManager;
    public RegisterUserEndpoint(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("ResgisterUser")]
    public override async Task<UserRegisterResponse> HandleAsync(UserRegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = new UserRegisterResponse();

        if (request == null) throw new WrongInfoUserForRegister();

        var resgiterUser = new ApplicationUser
        {
            Email = request.EmailAddress,
            UserAddress = request.Address,
            PhoneNumber = request.PhoneNumber,
            UserName = request.Name,
            UserFamily = request.Family,
        };

        var result = await _userManager.CreateAsync(resgiterUser, request.Password);

        if (result.Succeeded)
        {
            response.Message = "ثبت نام شما با موفقیت انجام شده. لطفا برای تایید حساب کاربری خود به صندوق دریافتی ایمیل خود رفته و بر بروی لینک فرستاده شده کلیک نمایید ";
        }
        else
        {
            var d = result.Errors;

            //| | | |
            //v v v v
            //Shuld be re work 
            response.Message = result.Errors.FirstOrDefault().ToString();
        }
        response.Result = result.Succeeded;
        response.Username = request.Name;

        return response;
    }
}
