using Microsoft.AspNetCore.SignalR;
using SignalR.API.DataService;
using SignalR.API.Models;

namespace SignalR.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SharedDb _sharedDb;

        public ChatHub(SharedDb sharedDb) => _sharedDb = sharedDb;

        public override async Task OnConnectedAsync()
        {
            await Clients.All
                .SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }
        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All
                .SendAsync("ReceiveMessage", "admin", $"{connection.UserName} has joined");
        }

        public async Task JoinSpecificChatRoom(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

            _sharedDb.connections[Context.ConnectionId] = connection;

            await Clients.Group(connection.ChatRoom)
                .SendAsync("ReceiveMessage", "admin", $"{connection.UserName} has joined {connection.ChatRoom}");
        }

        public async Task SendMessage(string msg)
        {
            if (_sharedDb.connections.TryGetValue(Context.ConnectionId, out UserConnection connection))
            {
                await Clients.Group(connection.ChatRoom)
                    .SendAsync("ReceiceSpectificMessage", connection.UserName, msg);
            }
        }
    }
}
