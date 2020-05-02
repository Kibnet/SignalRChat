using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR.EasyUse.Server;
using SignalRChat.Interface;

namespace SignalRChat.Server.Hubs
{
    //EasyUse realization IChatHub
    public class ChatHub : Hub, IChatHub
    {
        public async Task SendMessage(string user, string message)
        {
            //Native send message
            //await Clients.All.SendAsync("ReceiveMessage", user, message);

            //EasyUse send message
            await Clients.All.SendAsync(new ReceiveMessage
            {
                User = user,
                Message = message,
            });
        }
    }
}
