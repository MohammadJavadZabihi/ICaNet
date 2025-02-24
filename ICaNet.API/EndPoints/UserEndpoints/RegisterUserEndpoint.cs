using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs.Email;
using ICaNer.Shared.DTOs.UserRegister;
using ICaNet.ApplicationCore.Interfaces;
using ICaNet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ICaNet.API.EndPoints.UserEndpoints;

[Route("api/v{version:apiversion}/User")]
[ApiVersion("1.0")]
public class RegisterUserEndpoint : EndpointBaseAsync
    .WithRequest<UserRegisterRequest>
    .WithResult<UserRegisterResponse>
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    public RegisterUserEndpoint(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IEmailService emailService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    [HttpPost("Resgister")]
    public override async Task<UserRegisterResponse> HandleAsync(UserRegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = new UserRegisterResponse();

        if (request == null) throw new WrongInfoUserForRegister();

        var registerUser = new ApplicationUser
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

        var result = await _userManager.CreateAsync(registerUser, request.Password);

        if (result.Succeeded)
        {
            if (request.RegisterWithPhone)
            {
                response.Message = $"ثبت نام شما با موفقیت انجام شده. لطفا برای تایید حساب کاربری خود کد ارسال شده به شماره موبایل {request.PhoneNumber} را وارد نمایید ";
            }
            else
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(registerUser);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var domain = _configuration["AppSetting:Domain"];

                var confrimUrl = Url.Action(nameof(ConfirmEmail), "RegisterUserEndpoint", new { userId = registerUser.Id, token = encodedToken}, protocol: "https", host: domain);

                var sendEmail = new SendEmailDTO(registerUser.Email, "تایید حساب کاربری آیکانت", confrimUrl);

                await _emailService.SendEmailAsync(sendEmail);

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

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new UserNotFoundExeption("");

        if (user.EmailConfirmed)
        {
            return BadRequest("ایمیل قبلا تایید شده است");
        }
        var confrimEmail = await _userManager.ConfirmEmailAsync(user, token);

        if (!confrimEmail.Succeeded)
        {
            return BadRequest("خطا در تایید ایمیل, لطفا دوباره تلاش نمایید");
        }

        return Ok("ایمیل شما با موفقیت ثبت شد");
    }
}
