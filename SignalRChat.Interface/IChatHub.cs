using System.Threading.Tasks;
using SignalR.EasyUse.Interface;

namespace SignalRChat.Interface
{
    public interface IChatHub: IServerMethods
    {
        Task SendMessage(string user, string message);
    }
}