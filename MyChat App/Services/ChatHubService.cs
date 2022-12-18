using Microsoft.AspNetCore.SignalR;
using MyChat_App.ViewModels.ChatHub.Requests;

namespace MyChat_App.Services
{
    public class ChatHubService : Hub
    {
        public async Task SendMessageAsync(int roomId, SendMessageRequest request)
        {
            
        }
    }
}
