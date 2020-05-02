using SignalR.EasyUse.Interface;

namespace SignalRChat.Interface
{
    public class ReceiveMessage: IClientMethod
    {
        public string User { get;set; }
        public string Message{ get;set; }
    }
}