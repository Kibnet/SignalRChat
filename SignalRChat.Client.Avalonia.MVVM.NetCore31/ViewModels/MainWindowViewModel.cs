using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using ReactiveUI;

namespace SignalRChat.Client.Avalonia.MVVM.NetCore31.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:53353/ChatHub")
                .Build();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };

            Messages = new ObservableCollection<string>();
            ConnectCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                _connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    var newMessage = $"{user}: {message}";
                    Messages.Add(newMessage);

                });

                try
                {
                    await _connection.StartAsync();
                    Messages.Add("Connection started");
                    IsConnected = true;
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Messages.Add(ex.Message);
                }
            } //, this.ObservableForProperty(m => m.IsConnected, b => !b)
              );
            
            SendCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                try
                {
                    await _connection.InvokeAsync("SendMessage",
                        UserName, MessageText);
                }
                catch (Exception ex)
                {
                    Messages.Add(ex.Message);
                }
            }//, this.ObservableForProperty(m => m.IsConnected, b => b == true)
                );
            IsConnected = false;
        }
        
        readonly HubConnection _connection;

        public bool IsConnected { get;set; }

        public ObservableCollection<string> Messages { get; set; }

        public string UserName { get; set; }

        public string MessageText { get; set; }

        public IReactiveCommand ConnectCommand { get; }

        public IReactiveCommand SendCommand { get; }
    }
}
