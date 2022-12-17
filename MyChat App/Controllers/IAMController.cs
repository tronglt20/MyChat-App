using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyChat_App.Services.IAM;
using MyChat_App.ViewModels.IAM.Requests;
using Utilities.DTOs;

namespace MyChat_App.Controllers
{
    [ApiController]
    [Route("api/iam")]
    public class IAMController : ControllerBase
    {
        private readonly IAMService _service;

        public IAMController(IAMService service)
        {
            _service = service;
        }

        [HttpPost("sign-up/request")]
        [AllowAnonymous]
        public async Task SignUpRequest([FromBody] SendSignupAccountRequest request)
        {
            await _service.SignUpRequestAsync(request);
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<LoginResult> SignIn([FromBody] SignInRequest request)
        {
            return await _service.SignInAsync(request);
        }
    }
}
