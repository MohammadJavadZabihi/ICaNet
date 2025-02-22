using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs;
using ICaNer.Shared.DTOs.UserRegister;
using ICaNet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ICaNet.API.EndPoints.UserEndpoints;

[Route("api/v{version:apiversion}/User")]
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

    [HttpPost("Resgister")]
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
            RegisterDate = DateTime.UtcNow,
            SubscriptionExpiryDate = DateTime.UtcNow,
            SubscriptionStartDate = DateTime.UtcNow,
        };

        var result = await _userManager.CreateAsync(resgiterUser, request.Password);

        if (result.Succeeded)
        {
            if (request.RegisterWithPhone)
            {
                response.Message = $"ثبت نام شما با موفقیت انجام شده. لطفا برای تایید حساب کاربری خود کد ارسال شده به شماره موبایل {request.PhoneNumber} را وارد نمایید ";
            }
            else
            {
                response.Message = "ثبت نام شما با موفقیت انجام شده. لطفا برای تایید حساب کاربری خود به صندوق دریافتی ایمیل خود رفته و بر بروی لینک فرستاده شده کلیک نمایید ";
            }
        }
        else
        {
            foreach (var error in result.Errors)
            {
                response.ErrorMessages.Add(error.Description + " " + error.Code);
            }
        }
        response.Result = result.Succeeded;
        response.Username = request.Name;
        response.Email = request.EmailAddress;

        return response;
    }
}
