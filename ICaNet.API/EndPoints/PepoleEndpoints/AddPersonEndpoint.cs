using Ardalis.ApiEndpoints;
using Asp.Versioning;
using ICaNer.Shared.DTOs;
using ICaNer.Shared.DTOs.Pepole;
using ICaNet.ApplicationCore.Exceptions;
using ICaNet.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ICaNet.API.EndPoints.PepoleEndpoints
{
    [Route("api/v{version:apiversion}/Person")]
    [ApiVersion("1.0")]
    [Authorize]
    public class AddPersonEndpoint : EndpointBaseAsync
        .WithRequest<AddPersonRequest>
        .WithResult<ResponseDto>
    {
        private readonly IPersonService _personService;
        public AddPersonEndpoint(IPersonService personService)
        {
            _personService = personService;   
        }

        [HttpPost("Add")]
        public async override Task<ResponseDto> HandleAsync(AddPersonRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = new ResponseDto();

                var userRequest = HttpContext.User;
                if (!userRequest.Identity.IsAuthenticated || userRequest == null)
                {
                    response.Result = false;
                    response.Message = "خطای سطح دسترسی";

                    return response;
                }

                string userId = userRequest.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    response.Result = false;
                    response.Message = "خطای سطح دسترسی";

                    return response;
                }

                var statuce = await _personService.AddPersonAsync(request.Name, request.PhoneNumber,
                    request.Address, request.Type, request.EmailAddress, userId);

                if(statuce)
                {
                    response.Result = true;
                    response.Message = "فرد با موفقیت اضافه شد";

                    return response;
                }

                response.Result = false;
                response.Message = "خطایی صورت گرفت لطفا بعدا دوباره تلاش فرمایید";

                return response;
            }
            catch(CustomeException ex)
            {
                return new ResponseDto
                {
                    Message = ex.Message,
                    Result = false
                };
            }
        }
    }
}
