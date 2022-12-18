using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyChat_App.Services;
using MyChat_App.ViewModels.ChatHub.Requests;

namespace MyChat_App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/chathub")]
    public class ChatHubController : Controller
    {
        private readonly ChatHubService _service;

        public ChatHubController(ChatHubService service)
        {
            _service = service;
        }

        [HttpPost("rooms/{roomId:int}/message")]
        public async Task SendMessage([FromRoute] int roomId, [FromBody] SendMessageRequest request)
        {
            await _service.SendMessageAsync(roomId, request);
        }
    }
}
