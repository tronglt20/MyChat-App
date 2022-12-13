using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyChat_App.ViewModels.IAM.Requests;

namespace MyChat_App.Controllers.IAM
{
    [ApiController]
    [Route("api/iam")]
    public class IAMController : ControllerBase
    {
        [HttpPost("sign-up/request")]
        [AllowAnonymous]
        public async Task SendSignUpRequest([FromBody] SendSignupAccountRequest request)
        {
        }
    }
}
