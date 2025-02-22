using Ardalis.ApiEndpoints;
using ICaNer.Shared.DTOs.Email;
using ICaNet.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ICaNet.API.EndPoints.EmailConfirmationEndpint;

[Route("api/{version:apiversion}/email")]
public class EmailConfirmationEndpint : EndpointBaseAsync
    .WithRequest<SendEmailDTO>
    .WithResult<EmailConfrimResponse>
{
    private readonly IEmailService _emailService;

    public EmailConfirmationEndpint(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public override async Task<EmailConfrimResponse> HandleAsync(SendEmailDTO request, CancellationToken cancellationToken = default)
    {
        var response = new EmailConfrimResponse();

        if(string.IsNullOrEmpty(request.Email))
        {
            response.Message = "ایمیل وارد شده نا معتبر است";
            return response;
        }

        try
        {
            await _emailService.SendEmailAsync(request);
            response.Success = true;
            response.Message = "ایمیل با موفقیت ارسال شد";
        }
        catch(Exception ex)
        {
            response.Success = false;
            response.Message = $"{ex.Message} : خطا در ارسال ایمیل";
        }

        return response;

    }
}

